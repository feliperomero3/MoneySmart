using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MoneySmart.Data;
using MoneySmart.IntegrationTests.Helpers;

namespace MoneySmart.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.json");

            builder.ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile(configPath);
            });

            builder.UseEnvironment("Development");

            builder.ConfigureTestServices(services =>
            {
                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();

                var provider = scope.ServiceProvider;

                try
                {
                    var context = provider.GetRequiredService<ApplicationDbContext>();

                    DatabaseHelper.InitializeTestDatabase(context);
                    DatabaseHelper.SeedTestDatabase(context);
                }
                catch (SqlException sqlException)
                {
                    var logger = provider.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    logger.LogError(sqlException, "An error occurred creating the test database.");

                    throw;
                }
            });
        }
    }
}
