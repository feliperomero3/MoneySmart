using System.Globalization;
using System.Threading;
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

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-UK");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-UK");

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

                    ApplicationDbInitializer.Initialize(context);
                    ApplicationDbSeedData.SeedAsync(context, userManager).GetAwaiter().GetResult();
                }
                catch (SqlException sqlException)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();

                    logger.LogError(sqlException, "An error occurred creating the Database.");

                    throw;
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
