using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PdvNetApp.Application.Services;
using PdvNetApp.Domain.Interfaces;
using PdvNetApp.Infra.Context;
using PdvNetApp.Infra.Options;
using PdvNetApp.Infra.Repositories;
using PdvNetApp.UI.ViewModels;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace PdvNetApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public IServiceProvider Services { get; }
        private AppDbContext _dbContext;

        public App()
        {
            var services = new ServiceCollection();

            // 🔹 Lê o appsettings.json na mesma pasta do executável
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // 🔹 Faz o bind da seção ConnectionStrings para sua classe de opções
            var connectionStringOptions = configuration
                .GetSection("ConnectionStrings")
                .Get<ConnectionStringOptions>();

            if (string.IsNullOrEmpty(connectionStringOptions?.DefaultConnection))
                throw new InvalidOperationException("A ConnectionString 'DefaultConnection' não foi encontrada no appsettings.json.");

            // 🔹 Registra o DbContext com a ConnectionString
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionStringOptions.DefaultConnection));

            // 🔹 Registra as camadas e ViewModels
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<ProdutoService>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<MainWindow>();

            Services = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            // 🔹 Cria e abre uma instância global do DbContext
            _dbContext = Services.GetRequiredService<AppDbContext>();

            try
            {
                _dbContext.Database.OpenConnection();
                Console.WriteLine("Conexão com o banco aberta com sucesso.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao abrir conexão: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
                return;
            }

            // 🔹 Abre a janela principal
            var mainWindow = Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // 🔹 Fecha a conexão apenas ao encerrar o app
            if (_dbContext != null)
            {
                _dbContext.Database.CloseConnection();
                _dbContext.Dispose();
                Console.WriteLine("🔻 Conexão com o banco encerrada.");
            }

            base.OnExit(e);
        }
    }
}
