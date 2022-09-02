using System;
using MoneySmart.IntegrationTests.Extensions;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using AngleSharp.Html.Dom;
using MoneySmart.IntegrationTests.Helpers;
using System.Collections.Generic;

namespace MoneySmart.IntegrationTests.Pages
{
    public class TransferTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public TransferTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _factory.ClientOptions.AllowAutoRedirect = false;
            _factory.ClientOptions.BaseAddress = new Uri("https://localhost:5001/Transfers/");
        }

        [Fact]
        public async Task Get_Create_Transfer_Returns_Page()
        {
            var client = _factory.CreateClientWithAuthenticatedUser();

            var response = await client.GetAsync(string.Empty);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Post_Create_Transfer_Redirects_To_Transaction_Index_Page_On_Success()
        {
            var client = _factory.CreateClientWithAuthenticatedUser();
            var defaultPage = await client.GetAsync(string.Empty);
            var content = await HtmlDocumentHelper.GetDocumentAsync(defaultPage);

            var form = (IHtmlFormElement)content.QuerySelector("form");
            var submit = (IHtmlInputElement)content.QuerySelector("input[type='submit']");

            var response = await client.SendAsync(form,
                submit,
                new Dictionary<string, string>
                {
                    ["TransferModel.DateTime"] = "2022-08-28 11:00",
                    ["TransferModel.SourceAccountId"] = "1",
                    ["TransferModel.DestinationAccountId"] = "2",
                    ["TransferModel.Description"] = "My transfer",
                    ["TransferModel.Amount"] = "100",
                    ["TransferModel.Notes"] = "My notes"
                });

            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/Transactions", response.Headers.Location.OriginalString);
        }

    }
}
