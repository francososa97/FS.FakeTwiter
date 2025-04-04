using FS.FakeTwiter.Domain.Entities;
using FS.FakeTwitter.Application.Interfaces;
using FS.FakeTwitter.Domain.Interfaces;
using FS.Framework.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace FS.FakeTwitter.Infrastructure.Repositories;

public class TweetRepository : ITweetRepository
{
    private readonly ApplicationDbContext _context;

    public TweetRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Tweet tweet)
    {
        _context.Tweets.Add(tweet);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Tweet>> GetByUserIdAsync(string userId)
    {
        return await _context.Tweets
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Tweet>> GetByUsersAsync(IEnumerable<string> userIds)
    {
        return await _context.Tweets
            .Where(t => userIds.Contains(t.UserId))
            .ToListAsync();
    }
}
