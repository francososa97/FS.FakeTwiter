
using FS.FakeTwiter.Domain.Entities;

namespace FS.FakeTwitter.Application.Interfaces;

public interface ITweetRepository
{
    Task AddAsync(Tweet tweet);
    Task<IEnumerable<Tweet>> GetByUserIdAsync(string userId);
    Task<IEnumerable<Tweet>> GetByUsersAsync(IEnumerable<string> userIds);
}
