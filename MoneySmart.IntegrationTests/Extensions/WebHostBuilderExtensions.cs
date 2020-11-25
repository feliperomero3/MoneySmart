using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

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
    }
}
