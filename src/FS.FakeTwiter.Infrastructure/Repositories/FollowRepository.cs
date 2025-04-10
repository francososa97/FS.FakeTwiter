using FS.FakeTwiter.Domain.Entities;
using FS.FakeTwitter.Application.Interfaces;
using FS.FakeTwiter.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace FS.FakeTwitter.Infrastructure.Repositories;

public class FollowRepository : IFollowRepository
{
    private readonly ApplicationDbContext _context;

    public FollowRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(FollowRelation relation)
    {
        _context.Follows.Add(relation);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<string>> GetFollowersAsync(string userId)
    {
        return await _context.Follows
            .Where(f => f.FolloweeId == userId)
            .Select(f => f.FollowerId)
            .ToListAsync();
    }

    public async Task<IEnumerable<string>> GetFollowingAsync(string userId)
    {
        return await _context.Follows
            .Where(f => f.FollowerId == userId)
            .Select(f => f.FolloweeId)
            .ToListAsync();
    }
    public async Task<bool> ExistsAsync(string followerId, string followeeId)
    {
        return await _context.Follows
            .AnyAsync(f => f.FollowerId == followerId && f.FolloweeId == followeeId);
    }
}
