using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PdvNetApp.Infra.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdvNetApp.UI
{
    public static class ConnectionTest
    {
        public static async Task TestarConexaoAsync()
        {
            try
            {
                // Lê o appsettings.json
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.json"))
                    .Build();

                // Configura os serviços do EF Core
                var services = new ServiceCollection();
                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

                var provider = services.BuildServiceProvider();

                // Tenta abrir uma conexão
                using var context = provider.GetRequiredService<AppDbContext>();
                Console.WriteLine("Testando conexão...");

                var canConnect = await context.Database.CanConnectAsync();

                if (canConnect)
                    Console.WriteLine("✅ Conexão com o banco de dados bem-sucedida!");
                else
                    Console.WriteLine("❌ Não foi possível conectar ao banco de dados.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🚨 Erro ao testar conexão: {ex.Message}");
            }
        }

    }
}
