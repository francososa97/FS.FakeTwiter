using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FS.FakeTwiter.Application.Features.Tweet.Commands.PostTweetCommand;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace FS.FakeTwiter.IntegrationTests.Controllers;

public class TweetControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public TweetControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostTweet_ShouldReturn_TweetId()
    {
        // Arrange
        var command = new PostTweetCommand
        {
            UserId = "test-user",
            Content = "Este es un tweet de prueba."
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/tweet", command);

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, Guid>>();
        Assert.True(result!.ContainsKey("tweetId"));
    }

    [Fact]
    public async Task GetUserTweets_ShouldReturn_TweetsForUser()
    {
        // Arrange
        var userId = "user-tweets";
        await _client.PostAsJsonAsync("/api/tweet", new PostTweetCommand
        {
            UserId = userId,
            Content = "Primer tweet"
        });

        await _client.PostAsJsonAsync("/api/tweet", new PostTweetCommand
        {
            UserId = userId,
            Content = "Segundo tweet"
        });

        // Act
        var response = await _client.GetAsync($"/api/tweet/user/{userId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var tweets = await response.Content.ReadFromJsonAsync<List<string>>();
        Assert.Equal(2, tweets!.Count);
    }

    [Fact]
    public async Task GetTimeline_ShouldIncludeTweetsFromFollowedUsers()
    {
        // Arrange
        var follower = "user-a";
        var followed = "user-b";

        // user-a sigue a user-b
        await _client.PostAsJsonAsync("/api/follow", new { FollowerId = follower, FolloweeId = followed });

        // user-b publica un tweet
        await _client.PostAsJsonAsync("/api/tweet", new PostTweetCommand
        {
            UserId = followed,
            Content = "Tweet de seguido"
        });

        // Act
        var response = await _client.GetAsync($"/api/tweet/timeline/{follower}");

        // Assert
        response.EnsureSuccessStatusCode();
        var timeline = await response.Content.ReadFromJsonAsync<List<string>>();
        Assert.Single(timeline);
        Assert.Contains("Tweet de seguido", timeline[0]);
    }
    [Fact]
    public async Task GetTimeline_ShouldIncludeTweetsFromFollowedUsersSimple()
    {
        try
        {
            var response = await _client.GetAsync("/api/tweet/timeline/user-a");

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine("❗ Response content: " + content);

            response.EnsureSuccessStatusCode(); // Falla acá si status 500

            var timeline = await response.Content.ReadFromJsonAsync<List<string>>();
            Assert.Single(timeline);
            Assert.Contains("Tweet de seguido", timeline[0]);
        }
        catch (Exception ex)
        {
            Console.WriteLine("🔥 Excepción: " + ex.Message);
            throw;
        }
    }


}
