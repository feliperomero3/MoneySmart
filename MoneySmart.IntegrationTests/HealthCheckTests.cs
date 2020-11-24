using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace MoneySmart.IntegrationTests
{
    public class HealthCheckTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;

        public HealthCheckTests(WebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateDefaultClient(new Uri("https://localhost:5001"));
        }

        [Fact]
        public async Task HealthCheck_returns_success()
        {
            var response = await _httpClient.GetAsync("/healthcheck");

            response.EnsureSuccessStatusCode();
        }
    }
}
