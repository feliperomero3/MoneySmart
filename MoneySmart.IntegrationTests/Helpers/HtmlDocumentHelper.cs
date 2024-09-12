using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Dom;

namespace MoneySmart.IntegrationTests.Helpers
{
    public static class HtmlDocumentHelper
    {
        public static async Task<IHtmlDocument> GetDocumentAsync(HttpResponseMessage response)
        {
            var contentStream = await response.Content.ReadAsStreamAsync();

            var browser = BrowsingContext.New();

            var document = await browser.OpenAsync(virtualResponse =>
            {
                virtualResponse.Content(contentStream, shouldDispose: true);
                virtualResponse.Address(response.RequestMessage!.RequestUri).Status(response.StatusCode);

                MapHeaders(response.Headers);
                MapHeaders(response.Content.Headers);

                void MapHeaders(HttpHeaders headers)
                {
                    foreach (var header in headers)
                    {
                        foreach (var value in header.Value)
                        {
                            virtualResponse.Header(header.Key, value);
                        }
                    }
                }
            });

            return (IHtmlDocument)document;
        }
    }
}
