using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;
using FS.FakeTwitter.IntegrationTests.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using FS.FakeTwiter.Application.Features.Tweet.Commands.PostTweetCommand;
using FS.FakeTwitter.Application.Features.Follows.Commands;
using System.Linq;

namespace FS.FakeTwitter.IntegrationTests.Controllers;

public class TweetControllerTests : IntegrationTestBase
{
    public TweetControllerTests(CustomWebApplicationFactory<Program> factory) : base(factory) { }

    [Fact]
    public async Task Should_Post_Tweet_Successfully()
    {
        await AuthorizeAsync();
        var userId = await CreateTestUser("tweet");

        var tweetCommand = new PostTweetCommand()
        {
            UserId = userId,
            Content = "Este es un tweet de prueba"
        };

        var response = await _client.PostAsJsonAsync("/api/tweet", tweetCommand);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadFromJsonAsync<JsonElement>();
        var tweetIdObj = json.GetProperty("tweetId");
        var tweetId = tweetIdObj.GetProperty("data").GetString();

        Assert.False(string.IsNullOrEmpty(tweetId));
    }

    [Fact]
    public async Task Should_Get_User_Tweets()
    {
        await AuthorizeAsync();
        var userId = await CreateTestUser("tweet-get");

        // Publicar un tweet
        var tweetCommand = new
        {
            UserId = userId,
            Content = "Tweet de prueba para verificar el GET"
        };
        var post = await _client.PostAsJsonAsync("/api/tweet", tweetCommand);
        post.EnsureSuccessStatusCode();

        // Consultar los tweets del usuario
        var response = await _client.GetAsync($"/api/tweet/user/{userId}");
        response.EnsureSuccessStatusCode();

        var tweets = await response.Content.ReadFromJsonAsync<List<string>>();
        Assert.NotNull(tweets);
        Assert.Contains("Tweet de prueba para verificar el GET", tweets);
    }

    [Fact]
    public async Task Should_Get_Timeline()
    {
        await AuthorizeAsync();

        var followedId = await CreateTestUser("timeline-followed");
        var followerId = await CreateTestUser("timeline-follower");

        // Seguir al usuario
        var followCommand = new FollowUserCommand()
        {
            FollowerId = followerId,
            FollowId = followedId
        };
        var follow = await _client.PostAsJsonAsync("/api/follow", followCommand);
        follow.EnsureSuccessStatusCode();

        // Publicar un tweet como seguido
        var tweet = await _client.PostAsJsonAsync("/api/tweet", new
        {
            UserId = followedId,
            Content = "Tweet desde seguido para timeline"
        });
        tweet.EnsureSuccessStatusCode();

        // Obtener timeline del follower
        var response = await _client.GetAsync($"/api/tweet/timeline/{followerId}");
        response.EnsureSuccessStatusCode();

        var timeline = await response.Content.ReadFromJsonAsync<List<string>>();
        Assert.Single(timeline);
        Assert.NotNull(timeline);
        Assert.Contains("Tweet desde seguido para timeline", timeline.FirstOrDefault());
    }
}
