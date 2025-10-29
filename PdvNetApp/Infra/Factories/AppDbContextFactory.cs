using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PdvNetApp.Infra.Context;
using System;
using System.IO;

namespace PdvNetApp.Infra.Factories
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Caminho para o appsettings.json do projeto WPF (UI)
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../PdvNetApp");

            // Procura o arquivo appsettings.json (copiado junto ao executável)
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException(
                    "A ConnectionString 'DefaultConnection' não foi encontrada no appsettings.json. " +
                    "Verifique se o arquivo está sendo copiado para o diretório de saída."
                );

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
