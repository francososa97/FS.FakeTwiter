using FS.FakeTwiter.Application.Interfaces.Tweets;
using FS.FakeTwiter.Domain.Entities;
using FS.FakeTwitter.Domain.Interfaces;

namespace FS.FakeTwitter.Infrastructure.Services;

public class TweetService : ITweetService
{
    private readonly IUnitOfWork _unitOfWork;

    public TweetService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> PostTweetAsync(string userId, string content)
    {
        var tweet = new Tweet
        {
            UserId = userId,
            Content = content,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Tweets.AddAsync(tweet);
        await _unitOfWork.SaveChangesAsync();

        return tweet.Id;
    }

    public async Task<IEnumerable<string>> GetUserTweetsAsync(string userId)
    {
        var tweets = await _unitOfWork.Tweets.GetByUserIdAsync(userId);
        return tweets.Select(t => t.Content);
    }

    public async Task<IEnumerable<string>> GetTimelineAsync(string userId)
    {
        var followees = await _unitOfWork.Follows.GetFollowingAsync(userId);
        var tweets = await _unitOfWork.Tweets.GetByUsersAsync(followees);
        return tweets.OrderByDescending(t => t.CreatedAt).Select(t => t.Content);
    }
}
