using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FS.FakeTwiter.Application.Features.Tweet.Commands.PostTweetCommand;
using Xunit;

namespace FS.FakeTwiter.IntegrationTests.Features.Tweet.Command;
public class TweetControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public TweetControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostTweet_Should_Return_ValidationException_When_Content_TooLong()
    {
        // Arrange
        var command = new PostTweetCommand
        {
            UserId = "user-long-tweet",
            Content = new string('a', 281) // 281 caracteres
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/tweet", command);

        // Assert
        Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);

        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.Contains("no puede superar los 280 caracteres", responseContent);
    }
}
