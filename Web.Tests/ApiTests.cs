using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace SiteSyncTracker.Web.Tests;

public sealed class ApiTests : IClassFixture<TestApplicationFactory>
{
    private readonly HttpClient _client;

    public ApiTests(TestApplicationFactory factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
    }

    [Fact]
    public async Task Health_is_public()
    {
        using var response = await _client.GetAsync("/api/health");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Inventory_requires_authentication()
    {
        using var response = await _client.GetAsync("/api/inventory");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Inventory_write_requires_authentication()
    {
        using var response = await _client.PostAsJsonAsync("/api/inventory", new
        {
            manifestNumber = "TEST-1",
            description = "Test item",
            quantityExpected = 1,
            quantityReceived = 0,
            currentStatusId = 1,
            currentLocationId = 1
        });
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
