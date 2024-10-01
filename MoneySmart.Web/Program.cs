using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MoneySmart.Data;

namespace MoneySmart
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var environment = services.GetRequiredService<IWebHostEnvironment>();

                if (environment.IsDevelopment())
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

                    try
                    {
                        ApplicationDbSeedData.SeedAsync(context, userManager).GetAwaiter().GetResult();
                    }
                    catch (SqlException sqlException)
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();

                        logger.LogError(sqlException, "An error occurred while connecting to the database.");
                    }
                }
            }

            host.Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseAzureAppServices();
                });
    }
}
