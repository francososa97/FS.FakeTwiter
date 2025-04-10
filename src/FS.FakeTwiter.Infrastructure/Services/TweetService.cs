using FS.FakeTwiter.Domain.Entities;
using FS.FakeTwitter.Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;
using FS.FakeTwiter.Application.Interfaces.Cache;

namespace FS.FakeTwitter.Infrastructure.Services;

public class TweetService : ITweetService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheHelper _cacheHelper;

    public TweetService(IUnitOfWork unitOfWork, ICacheHelper cacheHelper)
    {
        _unitOfWork = unitOfWork;
        _cacheHelper = cacheHelper;
    }

    public async Task<Guid> PostTweetAsync(string userId, string content)
    {
        var tweet = new Tweet { UserId = userId, Content = content };
        await _unitOfWork.Tweets.AddAsync(tweet);
        await _unitOfWork.SaveChangesAsync();

        // 🔁 Invalida el cache del timeline del autor
        _cacheHelper.RemoveTimelineCache(userId);

        // 🔁 Invalida el cache de todos los seguidores
        var followers = await _unitOfWork.Follows.GetFollowersAsync(userId);
        foreach (var followerId in followers)
        {
            _cacheHelper.RemoveTimelineCache(followerId);
        }

        return tweet.Id;
    }

    public async Task<IEnumerable<string>> GetUserTweetsAsync(string userId)
    {
        return await _cacheHelper.GetOrSetUserTweetsAsync(userId, async () =>
        {
            var tweets = await _unitOfWork.Tweets.GetByUserIdAsync(userId);
            return tweets.Select(t => t.Content);
        });
    }
    public async Task<IEnumerable<string>> GetTimelineAsync(string userId)
    {
        return await _cacheHelper.GetOrSetTimelineAsync(userId, async () =>
        {
            var followedUsers = await _unitOfWork.Follows.GetFollowingAsync(userId);
            var tweets = await _unitOfWork.Tweets.GetByUsersAsync(followedUsers);

            return tweets
                .OrderByDescending(t => t.CreatedAt)
                .Select(t => $"{t.UserId}: {t.Content}")
                .ToList();
        });
    }
}
