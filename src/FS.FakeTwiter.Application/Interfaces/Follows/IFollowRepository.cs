using FS.FakeTwiter.Domain.Entities;
using System.Threading.Tasks;

namespace FS.FakeTwitter.Application.Interfaces;

public interface IFollowRepository
{
    Task AddAsync(FollowRelation relation);
    Task<IEnumerable<string>> GetFollowersAsync(string userId);
    Task<IEnumerable<string>> GetFollowingAsync(string userId);
    Task<bool> ExistsAsync(string followerId, string followeeId);
}
