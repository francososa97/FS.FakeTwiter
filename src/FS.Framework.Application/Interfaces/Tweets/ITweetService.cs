namespace FS.FakeTwiter.Application.Interfaces.Tweets;

public interface ITweetService
{
    Task<Guid> PostTweetAsync(string userId, string content);
    Task<IEnumerable<string>> GetUserTweetsAsync(string userId);
    Task<IEnumerable<string>> GetTimelineAsync(string userId);
}
