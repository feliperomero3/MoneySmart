using System.Data.SqlClient;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MoneySmart.Data;

namespace MoneySmart
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();

                    ApplicationDbInitializer.Initialize(context);
                }
                catch (SqlException sqlException)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();

                    logger.LogError(sqlException, "An error occurred creating the DB.");

                    throw;
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
