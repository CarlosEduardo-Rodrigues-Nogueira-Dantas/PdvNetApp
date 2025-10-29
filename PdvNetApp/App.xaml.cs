using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PdvNetApp.Application.Services;
using PdvNetApp.Domain.Interfaces;
using PdvNetApp.Infra.Context;
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

        public App()
        {
            var services = new ServiceCollection();

            // 🔹 Lê o appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory) // ensures the folder is the app's exe folder
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            // 🔹 Registra o DbContext com a ConnectionString
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // 🔹 Registra as camadas
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<ProdutoService>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<MainWindow>();

            Services = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }

}
