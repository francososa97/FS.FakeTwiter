using FS.FakeTwiter.Domain.Entities;
using FS.FakeTwiter.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FS.FakeTwitter.Infrastructure.Repositories;

public class TweetRepository : ITweetRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<TweetRepository> _logger;

    public TweetRepository(ApplicationDbContext context, ILogger<TweetRepository> logger)
    {
        _context = context;
        _logger = logger;
    }


    public async Task AddAsync(Tweet tweet)
    {
        _context.Tweets.Add(tweet);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Tweet>> GetByUserIdAsync(string userId)
    {
        _logger.LogDebug("Buscando tweets del usuario {UserId}", userId);
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
