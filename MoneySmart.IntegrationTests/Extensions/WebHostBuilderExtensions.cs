using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using MoneySmart.Data;
using MoneySmart.IntegrationTests.Helpers;

namespace MoneySmart.IntegrationTests.Extensions
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder WithAuthenticatedUser(this IWebHostBuilder builder)
        {
            return builder.ConfigureTestServices(services =>
            {
                services.AddAuthentication("Test")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>("Test", options => { });
            });
        }

        public static IWebHostBuilder WithDatabaseReset(this IWebHostBuilder builder)
        {
            return builder.ConfigureTestServices(services =>
            {
                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();

                var provider = scope.ServiceProvider;

                var context = provider.GetRequiredService<ApplicationDbContext>();

                DatabaseHelper.ResetTestDatabase(context);
            });
        }
    }
}
