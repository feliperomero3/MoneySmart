using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Testing;

namespace MoneySmart.IntegrationTests.Extensions
{
    public static class WebApplicationFactoryExtensions
    {
        public static HttpClient CreateClientWithAuthenticatedUser(this WebApplicationFactory<Startup> factory)
        {
            var client = factory.WithWebHostBuilder(builder =>
            {
                builder.WithAuthenticatedUser();
            }).CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            return client;
        }
    }
}
