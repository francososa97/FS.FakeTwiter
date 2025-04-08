using FS.FakeTwitter.Application.Interfaces;
using FS.FakeTwitter.Domain.Entities;
using FS.FakeTwitter.Domain.Interfaces;
using global::FS.FakeTwiter.Infrastructure.DataAccess;

namespace FS.FakeTwiter.Infrastructure.UnitOfWork;


public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context,
        ITweetRepository tweetRepository,
        IFollowRepository followRepository,
        IUserRepository userRepository)
    {
        _context = context;
        Tweets = tweetRepository;
        Follows = followRepository;
        Users= userRepository;
    }

    public ITweetRepository Tweets { get; }
    public IFollowRepository Follows { get; }
    public IUserRepository Users { get; }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}
