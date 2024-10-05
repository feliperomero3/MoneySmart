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
using MoneySmart.IntegrationTests.Identity;
using MoneySmart.Services;

namespace MoneySmart.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.IntegrationTests.json");

            builder.ConfigureAppConfiguration((_, config) =>
            {
                config.AddJsonFile(configPath);
            });

            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<IEmailSender, TestEmailSender>();

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();

                var provider = scope.ServiceProvider;

                var context = provider.GetRequiredService<ApplicationDbContext>();

                try
                {
                    DatabaseHelper.InitializeTestDatabase(context);
                }
                catch (SqlException sqlException)
                {
                    var logger = provider.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    logger.LogError(sqlException, "An error occurred creating the test database.");

                    throw;
                }
            });

            builder.UseEnvironment("Development");
        }
    }
}
