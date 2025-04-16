using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using FS.FakeTwiter.Application.Interfaces.Cache;
using Moq;
using FS.FakeTwitter.Infrastructure.Services;
using FS.FakeTwitter.Application.Features.Follows.Queries;

[MemoryDiagnoser]
public class TweetQueryBenchmarks
{
    private GetFollowersQueryHandler _handler;

    [GlobalSetup]
    public void Setup()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockFollowRepo = new Mock<IFollowRepository>();
        var mockCache = new Mock<ICacheHelper>();

        mockFollowRepo.Setup(r => r.GetFollowersAsync(It.IsAny<string>()))
            .ReturnsAsync(Enumerable.Repeat("follower-id", 10));

        mockUnitOfWork.Setup(u => u.Follows).Returns(mockFollowRepo.Object);
        mockCache.Setup(c => c.GetOrSetFollowersAsync(
            It.IsAny<string>(),
            It.IsAny<Func<Task<IEnumerable<string>>>>()))
        .ReturnsAsync(Enumerable.Repeat("follower-id", 10));

        mockCache.Setup(c => c.GetOrSetFollowersAsync(It.IsAny<string>(), It.IsAny<Func<Task<IEnumerable<string>>>>()))
            .ReturnsAsync(Enumerable.Repeat("follower-id", 10));

        var followService = new FollowService(mockUnitOfWork.Object, mockCache.Object);
        _handler = new GetFollowersQueryHandler(followService);
    }

    [Benchmark]
    public async Task SimularMillonDeConsultas()
    {
        for (int i = 0; i < 1_000_000; i++)
        {
            await _handler.Handle(new GetFollowersQuery { UserId = "user-" + i }, default);
        }
    }
    [Benchmark]
    public async Task GetFollowers_1M()
    {
        for (int i = 0; i < 1_000_000; i++)
        {
            await _handler.Handle(new GetFollowersQuery { UserId = $"user-{i}" }, default);
        }
    }

    [Benchmark]
    public async Task GetFollowers_100K()
    {
        for (int i = 0; i < 100_000; i++)
        {
            await _handler.Handle(new GetFollowersQuery { UserId = $"user-{i}" }, default);
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<TweetQueryBenchmarks>();
    }
}