using FS.FakeTwiter.Domain.Entities;

namespace FS.FakeTwitter.Infrastructure.Services;

public class FollowService : IFollowService
{
    private readonly IUnitOfWork _unitOfWork;

    public FollowService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task FollowAsync(string followerId, string followeeId)
    {
        var follow = new FollowRelation
        {
            FollowerId = followerId,
            FolloweeId = followeeId,
            FollowedAt = DateTime.UtcNow
        };

        await _unitOfWork.Follows.AddAsync(follow);
        await _unitOfWork.SaveChangesAsync();
    }

    public Task<IEnumerable<string>> GetFollowersAsync(string userId)
    {
        return _unitOfWork.Follows.GetFollowersAsync(userId);
    }

    public Task<IEnumerable<string>> GetFollowingAsync(string userId)
    {
        return _unitOfWork.Follows.GetFollowingAsync(userId);
    }
    public Task<bool> ExistsAsync(string followerId, string followeeId)
    {
        return _unitOfWork.Follows.ExistsAsync(followerId, followeeId);
    }
}
