using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using System.Net.Http;
using System.Collections.Generic;
using FS.FakeTwitter.Application.Features.Follows.Commands;
using System.Diagnostics.CodeAnalysis;

namespace FS.FakeTwitter.IntegrationTests.Controllers;
[ExcludeFromCodeCoverage]
public class FollowControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>

{
    private readonly HttpClient _client;

    public FollowControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Should_Follow_User_Successfully()
    {
        var command = new FollowUserCommand
        {
            FollowerId = "user-1",
            FolloweeId = "user-2"
        };

        var response = await _client.PostAsJsonAsync("/api/follow", command);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Should_Return_Followers_Of_A_User()
    {
        await _client.PostAsJsonAsync("/api/follow", new FollowUserCommand
        {
            FollowerId = "user-10",
            FolloweeId = "user-20"
        });

        var response = await _client.GetAsync("/api/follow/followers/user-20");

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<List<string>>();

        Assert.Contains("user-10", result);
    }

    [Fact]
    public async Task Should_Return_Users_Followed_By_User()
    {
        await _client.PostAsJsonAsync("/api/follow", new FollowUserCommand
        {
            FollowerId = "user-30",
            FolloweeId = "user-40"
        });

        var response = await _client.GetAsync("/api/follow/following/user-30");

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<List<string>>();

        Assert.Contains("user-40", result);
    }
}
