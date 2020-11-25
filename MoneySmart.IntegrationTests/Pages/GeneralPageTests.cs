using System;
using System.Threading.Tasks;
using Xunit;

namespace MoneySmart.IntegrationTests.Pages
{
    public class GeneralPageTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public GeneralPageTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _factory.ClientOptions.AllowAutoRedirect = false;
            _factory.ClientOptions.BaseAddress = new Uri("https://localhost:5001/");
        }

        [Theory]
        [InlineData("")]
        [InlineData("Index")]
        [InlineData("Privacy")]
        public async Task Get_EndpointsReturnSuccessAndExpectedContentType(string path)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(path);

            response.EnsureSuccessStatusCode();

            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}
