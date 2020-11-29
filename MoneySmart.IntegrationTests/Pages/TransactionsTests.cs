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
    public class TransactionsTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public TransactionsTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _factory.ClientOptions.AllowAutoRedirect = false;
            _factory.ClientOptions.BaseAddress = new Uri("https://localhost:5001/Transactions/");
        }

        [Fact]
        public async Task Get_Create_Transaction_Returns_Page()
        {
            var client = _factory.CreateClientWithAuthenticatedUser();

            var response = await client.GetAsync("Create");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Post_Create_Transaction_Redirects_To_Index_Page_On_Success()
        {
            var client = _factory.CreateClientWithAuthenticatedUser();
            var defaultPage = await client.GetAsync("Create");
            var content = await HtmlHelper.GetDocumentAsync(defaultPage);

            var form = (IHtmlFormElement)content.QuerySelector("form");
            var submit = (IHtmlInputElement)content.QuerySelector("input[type='submit']");

            var response = await client.SendAsync(form,
                submit,
                new Dictionary<string, string>
                {
                    ["TransactionModel.DateTime"] = "2020-11-28 11:00",
                    ["TransactionModel.AccountId"] = "1",
                    ["TransactionModel.Description"] = "Coffee",
                    ["TransactionModel.TransactionTypeName"] = "Expense",
                    ["TransactionModel.Amount"] = "5"
                });

            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/Transactions", response.Headers.Location.OriginalString);
        }

        [Fact]
        public async Task Get_Edit_Transaction_Returns_Edit_Page_On_Success()
        {
            var client = _factory.CreateClientWithAuthenticatedUser();

            var response = await client.GetAsync("Edit/?id=1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Post_Edit_Transaction_Redirects_To_Index_Page_On_Success()
        {
            var client = _factory.CreateClientWithAuthenticatedUser();
            var defaultPage = await client.GetAsync("Edit/?id=1");
            var content = await HtmlHelper.GetDocumentAsync(defaultPage);

            var form = (IHtmlFormElement)content.QuerySelector("form");
            var submit = (IHtmlInputElement)content.QuerySelector("input[type='submit']");

            var response = await client.SendAsync(form, submit,
                new Dictionary<string, string>
                {
                    ["TransactionModel.DateTime"] = "2020-11-28 10:00",
                    ["TransactionModel.AccountId"] = "1",
                    ["TransactionModel.Description"] = "Coffee",
                    ["TransactionModel.TransactionTypeName"] = "Expense",
                    ["TransactionModel.Amount"] = "4.5"
                });

            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/Transactions", response.Headers.Location.OriginalString);
        }
    }
}
