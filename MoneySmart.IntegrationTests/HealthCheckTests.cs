using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MoneySmart.IntegrationTests
{
    public class HealthCheckTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;

        public HealthCheckTests(CustomWebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateDefaultClient(new Uri("https://localhost:5001"));
        }

        [Fact]
        public async Task HealthCheck_returns_success()
        {
            var response = await _httpClient.GetAsync("/health");

            response.EnsureSuccessStatusCode();
        }
    }
}
