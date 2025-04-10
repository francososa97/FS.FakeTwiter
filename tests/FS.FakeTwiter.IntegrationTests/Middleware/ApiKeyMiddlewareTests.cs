using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace FS.FakeTwiter.IntegrationTests.Middleware;

public class ApiKeyMiddlewareIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _factory;

    public ApiKeyMiddlewareIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task Should_Return_401_When_No_ApiKey_Provided()
    {
        // Act
        var response = await _client.GetAsync("/api/User");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_401_When_ApiKey_Is_Invalid()
    {
        // Act
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/User");
        request.Headers.Add("X-API-KEY", "wrong-api-key");

        var response = await _client.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Should_Call_Endpoint_With_Valid_ApiKey()
    {
        // Act
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/User");
        var login = new { Email = "admin", Password = "admin123" };
        var response = await _client.PostAsJsonAsync("/api/auth/login", login);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadFromJsonAsync<JsonElement>();
        var token = json.GetProperty("token").GetString();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var responseApi = await _client.GetAsync("/api/user");

        // Assert
        Assert.Equal(HttpStatusCode.OK, responseApi.StatusCode);
    }
}