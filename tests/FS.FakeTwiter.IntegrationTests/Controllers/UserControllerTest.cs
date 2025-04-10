using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;
using FS.FakeTwitter.IntegrationTests.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using FS.FakeTwiter.Application.DTOs;
using System.Linq;
using FS.FakeTwiter.Application.Features.Users.Commands.UpdateUserCommand;

namespace FS.FakeTwitter.IntegrationTests.Controllers;

public class UserControllerTests : IntegrationTestBase
{
    public UserControllerTests(CustomWebApplicationFactory<Program> factory) : base(factory) { }

    [Fact]
    public async Task Should_Create_User_Successfully()
    {
        await AuthorizeAsync();

        var createRequest = new
        {
            Email = $"user_@test.com",
            Username = "TestUser",
            FullName = "Test User"
        };

        var response = await _client.PostAsJsonAsync("/api/user", createRequest);
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
        Assert.True(jsonResponse.GetProperty("success").GetBoolean());
        Assert.Equal("Usuario creado exitosamente.", jsonResponse.GetProperty("message").GetString());
        var email = jsonResponse.GetProperty("data").GetProperty("email").GetString();
        Assert.StartsWith("user_", email);
        Assert.EndsWith("@test.com", email);
    }

    [Fact]
    public async Task Should_Get_User_By_Id()
    {
        await AuthorizeAsync();
        var userId = await CreateTestUser("getuser");

        var response = await _client.GetAsync($"/api/user/{userId}");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadFromJsonAsync<JsonElement>();

        var userIdFromResponse = json.GetProperty("data").GetProperty("id").GetString();
        Assert.Equal(userId, userIdFromResponse);
    }

    [Fact]
    public async Task Should_Get_All_Users()
    {
        await AuthorizeAsync();
        await CreateTestUser("getall");

        var response = await _client.GetAsync("/api/user");
        response.EnsureSuccessStatusCode();
        var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

        var users = jsonResponse.GetProperty("data").EnumerateArray()
                                .Select(user => new
                                {
                                    Id = user.GetProperty("id").GetString(),
                                    Username = user.GetProperty("username").GetString(),
                                    Email = user.GetProperty("email").GetString()
                                })
                                .ToList();

        Assert.NotEmpty(users);
    }

    [Fact]
    public async Task Should_NotFound_Update_User()
    {
        await AuthorizeAsync();
        var updateRequest = new
        {
            Id = Guid.NewGuid(),
            Email = $"updated_Update@test.com",
            Username = "UpdatedUser",
            FullName = "Updated Name"
        };

        var response = await _client.PutAsJsonAsync($"/api/user/{updateRequest.Id}", updateRequest);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    [Fact]
    public async Task Should_Update_User()
    {
        await AuthorizeAsync();
        var userId = await CreateTestUser("update");

        var updateRequest = new UpdateUserCommand(
            Id: Guid.Parse(userId),
            Email: $"updated_Update@test.com",
            Username: "UpdatedUser"
        );

        var response = await _client.PutAsJsonAsync($"/api/user/{userId}", updateRequest);
        var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();
        var user = jsonResponse.GetProperty("data");

        var id = user.GetProperty("id").GetString();
        var username = user.GetProperty("username").GetString();
        var email = user.GetProperty("email").GetString();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(userId, id);
        Assert.Equal("UpdatedUser", username);
        Assert.Equal("updated_Update@test.com", email);

    }

    [Fact]
    public async Task Should_Delete_User()
    {
        await AuthorizeAsync();
        var userId = await CreateTestUser("delete");

        var response = await _client.DeleteAsync($"/api/user/{userId}");
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}
