using FS.FakeTwiter.Application.Interfaces.Cache;
using FS.FakeTwiter.Domain.Entities;

namespace FS.FakeTwitter.Infrastructure.Services;

public class FollowService : IFollowService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheHelper _cacheHelper;

    public FollowService(IUnitOfWork unitOfWork, ICacheHelper cacheHelper)
    {
        _unitOfWork = unitOfWork;
        _cacheHelper = cacheHelper;
    }

    public async Task FollowAsync(string followerId, string followeeId)
    {
        var relation = new FollowRelation { FollowerId = followerId, FolloweeId = followeeId };
        await _unitOfWork.Follows.AddAsync(relation);
        await _unitOfWork.SaveChangesAsync();

        _cacheHelper.RemoveFollowersCache(followeeId);
        _cacheHelper.RemoveFollowingCache(followerId);
    }

    public async Task<IEnumerable<string>> GetFollowersAsync(string userId)
    {
        return await _cacheHelper.GetOrSetFollowersAsync(userId, () => _unitOfWork.Follows.GetFollowersAsync(userId));
    }

    public async Task<IEnumerable<string>> GetFollowingAsync(string userId)
    {
        return await _cacheHelper.GetOrSetFollowingAsync(userId, () => _unitOfWork.Follows.GetFollowingAsync(userId));
    }

    public async Task<bool> ExistsAsync(string followerId, string followeeId)
    {
        return await _unitOfWork.Follows.ExistsAsync(followerId, followeeId);
    }
}
