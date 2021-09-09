using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using MoneySmart.IntegrationTests.Extensions;
using MoneySmart.IntegrationTests.Helpers;
using Xunit;

namespace MoneySmart.IntegrationTests.Pages
{
    public class AccountsTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public AccountsTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _factory.ClientOptions.AllowAutoRedirect = false;
            _factory.ClientOptions.BaseAddress = new Uri("https://localhost:5001/Accounts/");
        }

        [Fact]
        public async Task Get_Index_Account_Returns_Accounts()
        {
            var client = _factory.CreateClientWithAuthenticatedUser();

            var response = await client.GetAsync("");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_Create_Account_Returns_Page()
        {
            var client = _factory.CreateClientWithAuthenticatedUser();

            var response = await client.GetAsync("Create");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_Account_Details_Returns_Account_Page()
        {
            var client = _factory.CreateClientWithAuthenticatedUser();

            var response = await client.GetAsync("Details/5221");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_Non_Existing_Account_Details_Returns_Not_Found()
        {
            var client = _factory.CreateClientWithAuthenticatedUser();

            var response = await client.GetAsync("Details/0000");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Get_Account_Details_Omitting_Number_Parameter_Returns_Not_Found()
        {
            var client = _factory.CreateClientWithAuthenticatedUser();

            var response = await client.GetAsync("Details");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Get_Edit_Account_Returns_Edit_Account_Page()
        {
            var client = _factory.CreateClientWithAuthenticatedUser();

            var response = await client.GetAsync("Edit/5221");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Post_Edit_Account_Redirects_To_Index_Page_On_Success()
        {
            var client = _factory.CreateClientWithAuthenticatedUser();
            var defaultPage = await client.GetAsync("Edit/5221");
            var content = await HtmlDocumentHelper.GetDocumentAsync(defaultPage);

            var form = (IHtmlFormElement)content.QuerySelector("form");
            var submit = (IHtmlInputElement)content.QuerySelector("input[type='submit']");

            var response = await client.SendAsync(form, submit,
                new Dictionary<string, string>
                {
                    ["AccountModel.Number"] = "5221",
                    ["AccountModel.Name"] = "New Name"
                });

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/Accounts", response.Headers.Location.OriginalString);
        }

        [Fact]
        public async Task Get_Delete_Account_Returns_Delete_Account_Page()
        {
            var client = _factory.CreateClientWithAuthenticatedUser();

            var response = await client.GetAsync("Delete/5221");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Post_Delete_Account_Redirects_To_Index_Page_On_Success()
        {
            var client = _factory.CreateClientWithAuthenticatedUser();
            var defaultPage = await client.GetAsync("Delete/2152");
            var content = await HtmlDocumentHelper.GetDocumentAsync(defaultPage);

            var form = (IHtmlFormElement)content.QuerySelector("form");
            var submit = (IHtmlInputElement)content.QuerySelector("input[type='submit']");

            var response = await client.SendAsync(form, submit,
                new Dictionary<string, string>
                {
                    ["AccountModel.Number"] = "2152"
                });

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/Accounts", response.Headers.Location.OriginalString);
        }
    }
}
