using FS.FakeTwiter.Application.Interfaces.Cache;
using Microsoft.Extensions.Caching.Memory;

namespace FS.FakeTwiter.Infrastructure.Cache;

public class CacheHelper : ICacheHelper
{
    private readonly IMemoryCache _cache;
    private const int DefaultTtlSeconds = 60;

    public CacheHelper(IMemoryCache cache)
    {
        _cache = cache;
    }

    private static MemoryCacheEntryOptions BuildCacheOptions(int ttlSeconds)
    {
        return new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(ttlSeconds),
            SlidingExpiration = TimeSpan.FromSeconds(ttlSeconds / 2)
        };
    }

    public async Task<IEnumerable<string>> GetOrSetFollowersAsync(string userId, Func<Task<IEnumerable<string>>> factory)
    {
        var key = $"followers:{userId}";
        if (_cache.TryGetValue(key, out var obj) && obj is IEnumerable<string> cached)
            return cached;

        var value = await factory();
        _cache.Set(key, value, BuildCacheOptions(DefaultTtlSeconds));
        return value;
    }

    public async Task<IEnumerable<string>> GetOrSetFollowingAsync(string userId, Func<Task<IEnumerable<string>>> factory)
    {
        var key = $"following:{userId}";
        if (_cache.TryGetValue(key, out var obj) && obj is IEnumerable<string> cached)
            return cached;

        var value = await factory();
        _cache.Set(key, value, BuildCacheOptions(DefaultTtlSeconds));
        return value;
    }

    public void RemoveFollowersCache(string userId)
    {
        _cache.Remove($"followers:{userId}");
    }

    public void RemoveFollowingCache(string userId)
    {
        _cache.Remove($"following:{userId}");
    }
    public async Task<IEnumerable<string>> GetOrSetTimelineAsync(string userId, Func<Task<IEnumerable<string>>> factory)
    {
        var key = $"timeline:{userId}";
        if (_cache.TryGetValue(key, out var obj) && obj is IEnumerable<string> cached)
            return cached;

        var value = await factory();
        _cache.Set(key, value, BuildCacheOptions(DefaultTtlSeconds));
        return value;
    }

    public void RemoveTimelineCache(string userId)
    {
        _cache.Remove($"timeline:{userId}");
    }

    public async Task<IEnumerable<string>> GetOrSetUserTweetsAsync(string userId, Func<Task<IEnumerable<string>>> factory)
    {
        var key = $"usertweets:{userId}";
        var value = await factory();
        _cache.Set(key, value, BuildCacheOptions(DefaultTtlSeconds));
        return value;
    }

    public void RemoveUserTweetsCache(string userId)
    {
        var key = $"usertweets:{userId}";
        _cache.Remove(key);
    }


}
