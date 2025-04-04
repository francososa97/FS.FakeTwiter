using FS.FakeTwitter.Application.Interfaces.Tweets;
using FS.FakeTwitter.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using FS.FakeTwiter.Application.Interfaces.Tweets;

namespace FS.FakeTwitter.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TweetController : ControllerBase
{
    private readonly ITweetService _tweetService;

    public TweetController(ITweetService tweetService)
    {
        _tweetService = tweetService;
    }

    [HttpPost]
    public async Task<IActionResult> PostTweet([FromBody] TweetCreateDto tweet)
    {
        var tweetId = await _tweetService.PostTweetAsync(tweet.UserId, tweet.Content);
        return CreatedAtAction(nameof(GetUserTweets), new { userId = tweet.UserId }, new { tweetId });
    }

    [HttpGet("timeline/{userId}")]
    public async Task<IActionResult> GetTimeline(string userId)
    {
        var timeline = await _tweetService.GetTimelineAsync(userId);
        return Ok(timeline);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserTweets(string userId)
    {
        var tweets = await _tweetService.GetUserTweetsAsync(userId);
        return Ok(tweets);
    }
}
