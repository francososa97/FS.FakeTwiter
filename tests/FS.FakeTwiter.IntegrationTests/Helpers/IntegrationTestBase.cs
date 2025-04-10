using System.Net.Http;
using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using FS.FakeTwiter.Application.Features.Users.Commands.CreateUserCommand;

namespace FS.FakeTwitter.IntegrationTests.Helpers;

public abstract class IntegrationTestBase : IClassFixture<CustomWebApplicationFactory<Program>>
{
    public readonly HttpClient _client;

    public IntegrationTestBase(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    public async Task AuthorizeAsync()
    {
        var login = new { Email = "admin", Password = "admin123" };
        var response = await _client.PostAsJsonAsync("/api/auth/login", login);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadFromJsonAsync<JsonElement>();
        var token = json.GetProperty("token").GetString();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    protected async Task<string> CreateTestUser(string prefix)
    {
        var userRequest = new CreateUserCommand(
            Email: $"{prefix}@test.com",
            Username: $"{prefix}_user"
        );

        var response = await _client.PostAsJsonAsync("/api/user", userRequest);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadFromJsonAsync<JsonElement>();
        var data = json.GetProperty("data");
        var id = data.GetProperty("id").GetString();

        return id;
    }
}
