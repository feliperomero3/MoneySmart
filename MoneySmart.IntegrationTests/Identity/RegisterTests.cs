using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MoneySmart.IntegrationTests.Extensions;
using Xunit;

namespace MoneySmart.IntegrationTests.Identity
{
    public class RegisterTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public RegisterTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _factory.ClientOptions.AllowAutoRedirect = false;
            _factory.ClientOptions.BaseAddress = new Uri("https://localhost:5001/");
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task Get_Register_Page_Returns_Redirect_For_Unauthenticated_User()
        {
            var response = await _client.GetAsync("Identity/Account/Register");

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        }

        [Fact]
        public async Task Get_Register_Page_Returns_Success_For_Authenticated_User()
        {
            var client = _factory.CreateClientWithAuthenticatedUser();

            var response = await client.GetAsync("Identity/Account/Register");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
