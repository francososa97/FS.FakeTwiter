using FS.FakeTwitter.Application.Interfaces;
using FS.FakeTwitter.Domain.Interfaces;
using global::FS.FakeTwiter.Infrastructure.DataAccess;

namespace FS.FakeTwiter.Infrastructure.UnitOfWork;


public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context,
        ITweetRepository tweetRepository,
        IFollowRepository followRepository)
    {
        _context = context;
        Tweets = tweetRepository;
        Follows = followRepository;
    }

    public ITweetRepository Tweets { get; }
    public IFollowRepository Follows { get; }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}
