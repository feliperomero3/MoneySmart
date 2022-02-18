using System;
using System.Net;
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
        public async Task Get_EndpointsReturnSuccessAndExpectedContentType(string path)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(path);

            response.EnsureSuccessStatusCode();

            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task Get_Register_Page_Returns_Redirect_For_Unauthenticated_User()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("Identity/Account/Register");

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        }
    }
}
