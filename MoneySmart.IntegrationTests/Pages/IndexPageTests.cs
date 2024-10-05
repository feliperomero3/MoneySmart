using System;
using System.Net;
using System.Threading.Tasks;
using MoneySmart.IntegrationTests.Extensions;
using Xunit;

namespace MoneySmart.IntegrationTests.Pages;

public class IndexPageTests : IClassFixture<CustomWebApplicationFactory<Startup>>
{
    private readonly CustomWebApplicationFactory<Startup> _factory;

    public IndexPageTests(CustomWebApplicationFactory<Startup> factory)
    {
        _factory = factory;
        _factory.ClientOptions.AllowAutoRedirect = false;
        _factory.ClientOptions.BaseAddress = new Uri("https://localhost:5001/");
    }

    [Theory]
    [InlineData("")]
    [InlineData("Index")]
    public async Task Get_Index_Page_Returns_Success(string path)
    {
        var client = _factory.CreateClientWithAuthenticatedUser();

        var response = await client.GetAsync(path);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}