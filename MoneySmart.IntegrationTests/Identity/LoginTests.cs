using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using MoneySmart.IntegrationTests.Extensions;
using MoneySmart.IntegrationTests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace MoneySmart.IntegrationTests.Identity
{
    public class LoginTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly ITestOutputHelper _output;

        public LoginTests(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output)
        {
            _factory = factory;
            _factory.ClientOptions.AllowAutoRedirect = false;
            _factory.ClientOptions.BaseAddress = new Uri("https://localhost:5001/");
            _output = output;
        }

        [Fact]
        public async Task Get_Login_Page_Returns_Success()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("Identity/Account/Login");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Post_Login_Page_Without_CSRF_Token_Returns_BadRequest()
        {
            var client = _factory.CreateClient();

            var response = await client.PostAsync("Identity/Account/Login", new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Input.Email", "admin@localhost"),
                new KeyValuePair<string, string>("Input.Password", "Secret123$")
            }));

            _output.WriteLine(response.StatusCode.ToString());

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_Login_Page_Successful_Login_Returns_Redirect()
        {
            var client = _factory.CreateClient();

            var defaultPage = await client.GetAsync("Identity/Account/Login");

            var content = await HtmlDocumentHelper.GetDocumentAsync(defaultPage);

            var form = (IHtmlFormElement)content.QuerySelector("form");
            var submit = (IHtmlElement)content.QuerySelector("[type='submit']");

            var response = await client.SendAsync(form, submit,
                new Dictionary<string, string>
                {
                    ["Input.Email"] = "admin@example.com",
                    ["Input.Password"] = "Secret123$"
                });

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        }
    }
}
