using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using MoneySmart.IntegrationTests.Extensions;
using MoneySmart.IntegrationTests.Helpers;
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

        [Fact]
        public async Task Post_Register_Page_Without_CSRF_Token_Returns_BadRequest()
        {
            var client = _factory.CreateClientWithAuthenticatedUser();

            var response = await client.PostAsync("Identity/Account/Register", new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Input.Email", "admin@localhost"),
                new KeyValuePair<string, string>("Input.Password", "Secret123$")
            }));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_Register_Page_Successful_Registration_Returns_Redirect()
        {
            var client = _factory.CreateClientWithAuthenticatedUser();

            var page = await client.GetAsync("Identity/Account/Register");

            var content = await HtmlDocumentHelper.GetDocumentAsync(page);

            var form = (IHtmlFormElement)content.QuerySelector("form");
            var submit = (IHtmlElement)content.QuerySelector("[type='submit']");

            var response = await client.SendAsync(form, submit,
                new Dictionary<string, string>
                {
                    ["Input.Email"] = "user@example.com",
                    ["Input.Password"] = "Secret123$",
                    ["Input.ConfirmPassword"] = "Secret123$"
                });

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        }
    }
}
