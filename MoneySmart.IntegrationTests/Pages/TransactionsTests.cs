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
        public async Task Get_Index_Transaction_Returns_Transactions_collection()
        {
            var client = _factory.CreateClientWithAuthenticatedUser();

            var response = await client.GetAsync("");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            using var content = await HtmlDocumentHelper.GetDocumentAsync(response);

            var transactions = content.QuerySelector("#transactions");

            Assert.NotNull(transactions);

            var rows = transactions.QuerySelectorAll(".row");

            Assert.NotEmpty(rows);
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
            var content = await HtmlDocumentHelper.GetDocumentAsync(defaultPage);

            var form = (IHtmlFormElement)content.QuerySelector("form");
            var submit = (IHtmlInputElement)content.QuerySelector("input[type='submit']");

            var response = await client.SendAsync(form,
                submit,
                new Dictionary<string, string>
                {
                    ["TransactionCreateModel.DateTime"] = "2020-11-28 11:00",
                    ["TransactionCreateModel.AccountId"] = "1",
                    ["TransactionCreateModel.Description"] = "Coffee",
                    ["TransactionCreateModel.TransactionType"] = "Expense",
                    ["TransactionCreateModel.Amount"] = "5"
                });

            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/Transactions", response.Headers.Location?.OriginalString);
        }

        [Fact]
        public async Task Get_Edit_Transaction_Returns_Edit_Page_On_Success()
        {
            var client = _factory.CreateClientWithAuthenticatedUser();

            var response = await client.GetAsync("Edit/1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Post_Edit_Transaction_Redirects_To_Index_Page_On_Success()
        {
            var client = _factory.CreateClientWithAuthenticatedUser();
            var defaultPage = await client.GetAsync("Edit/1");
            var content = await HtmlDocumentHelper.GetDocumentAsync(defaultPage);

            var form = (IHtmlFormElement)content.QuerySelector("form");
            var submit = (IHtmlInputElement)content.QuerySelector("input[type='submit']");

            var response = await client.SendAsync(form, submit,
                new Dictionary<string, string>
                {
                    ["TransactionEditModel.DateTime"] = "2020-11-28 10:00",
                    ["TransactionEditModel.AccountId"] = "2",
                    ["TransactionEditModel.Description"] = "Coffee",
                    ["TransactionEditModel.TransactionType"] = "Expense",
                    ["TransactionEditModel.Amount"] = "4.5"
                });

            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/Transactions", response.Headers.Location!.OriginalString);
        }

        [Fact]
        public async Task Get_Create_Transaction_Defaults_TransactionType_To_Expense()
        {
            var client = _factory.CreateClientWithAuthenticatedUser();
            var createPage = await client.GetAsync("Create");
            var content = await HtmlDocumentHelper.GetDocumentAsync(createPage);

            var select = (IHtmlSelectElement)content.QuerySelector("#TransactionCreateModel_TransactionType");

            Assert.NotNull(select);

            var selectedOption = select.Options[select.Options.SelectedIndex];

            Assert.Equal("Expense", selectedOption.Value);
        }

        [Fact]
        public async Task Get_Transaction_Details_Returns_Transaction_Details_Page()
        {
            var client = _factory.CreateClientWithAuthenticatedUser();

            var response = await client.GetAsync("Details/1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_Transaction_Details_Returns_NotFound()
        {
            var client = _factory.CreateClientWithAuthenticatedUser();

            var response = await client.GetAsync("Details/0");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Get_Transactions_Returns_TransactionDetails_PartialView()
        {
            var client = _factory.CreateClientWithAuthenticatedUser();

            var response = await client.GetAsync("?handler=TransactionDetails&id=1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
