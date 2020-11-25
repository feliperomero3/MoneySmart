using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MoneySmart.Data;
using MoneySmart.IntegrationTests.Helpers;

namespace MoneySmart.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private readonly InMemoryDatabaseRoot _dbRoot = new InMemoryDatabaseRoot();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Development");

            builder.ConfigureTestServices(services =>
            {
                var descriptor = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryTestDb", _dbRoot);
                });

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();

                var provider = scope.ServiceProvider;

                try
                {
                    var context = provider.GetRequiredService<ApplicationDbContext>();

                    DatabaseHelper.InitializeTestDatabase(context);
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
