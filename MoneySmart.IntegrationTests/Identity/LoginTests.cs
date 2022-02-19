using System;
using System.Threading.Tasks;
using Xunit;

namespace MoneySmart.IntegrationTests.Identity
{
    public class LoginTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public LoginTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _factory.ClientOptions.AllowAutoRedirect = false;
            _factory.ClientOptions.BaseAddress = new Uri("https://localhost:5001/");
        }

        [Fact]
        public async Task Get_Login_Returns_Success()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(string.Empty);

            response.EnsureSuccessStatusCode();

            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}
