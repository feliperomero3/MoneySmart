using System;
using System.Net;
using System.Threading.Tasks;
using MoneySmart.IntegrationTests.Extensions;
using Xunit;

namespace MoneySmart.IntegrationTests.Pages
{
    public class AccountsTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public AccountsTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _factory.ClientOptions.BaseAddress = new Uri("https://localhost:5001/Accounts/");
        }

        [Fact]
        public async Task Get_Create_Account_Returns_Page()
        {
            var client = _factory.CreateClientWithAuthenticatedUser();

            var response = await client.GetAsync("Create");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
