using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FS.FakeTwitter.Application.Features.Follows.Commands;
using FS.FakeTwitter.IntegrationTests.Helpers;
using Xunit;

namespace FS.FakeTwitter.IntegrationTests.Controllers;

public class FollowControllerTests : IntegrationTestBase
{
    public FollowControllerTests(CustomWebApplicationFactory<Program> factory) : base(factory) { }

    [Fact]
    public async Task Should_Not_Allow_Duplicate_Follow()
    {
        await AuthorizeAsync();

        var followerId = await CreateTestUser("test1");
        var FollowId = await CreateTestUser("test2");

        var followCommand = new FollowUserCommand()
        {
            FollowerId = followerId,
            FollowId = FollowId
        };

        // Primer follow debería ser exitoso
        var firstResponse = await _client.PostAsJsonAsync("/api/follow", followCommand);
        firstResponse.EnsureSuccessStatusCode();

        // Segundo follow debe fallar
        var secondResponse = await _client.PostAsJsonAsync("/api/follow", followCommand);
        var content = await secondResponse.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.UnprocessableContent, secondResponse.StatusCode);
        Assert.Contains("ya sigue a", content); // También podrías verificar el string completo
    }
    [Fact]
    public async Task Should_Follow_User_Successfully()
    {
        await AuthorizeAsync();
        var followerId = await CreateTestUser("follower_success");
        var FollowId = await CreateTestUser("following_success");

        var followCommand = new FollowUserCommand()
        {
            FollowerId = followerId,
            FollowId = FollowId
        };

        var response = await _client.PostAsJsonAsync("/api/follow", followCommand);
        var content = await response.Content.ReadAsStringAsync();

        Assert.True(response.IsSuccessStatusCode, $"Expected 200 OK. Got {response.StatusCode}. Content: {content}");
    }

    [Fact]
    public async Task Should_Return_Followers()
    {
        await AuthorizeAsync();

        var userId = await CreateTestUser("target");
        var followerId = await CreateTestUser("follower");

        await _client.PostAsJsonAsync("/api/follow", new
        {
            FollowerId = followerId,
            FollowingId = userId
        });

        var response = await _client.GetAsync($"/api/follow/followers/{userId}");
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_Following()
    {
        await AuthorizeAsync();

        var userId = await CreateTestUser("main");
        var followedId = await CreateTestUser("followed");

        await _client.PostAsJsonAsync("/api/follow", new
        {
            FollowerId = userId,
            FollowingId = followedId
        });

        var response = await _client.GetAsync($"/api/follow/following/{userId}");
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
