namespace FS.FakeTwitter.Application.Interfaces.Follows;

public interface IFollowService
{
    Task FollowAsync(string followerId, string followeeId);
    Task<IEnumerable<string>> GetFollowersAsync(string userId);
    Task<IEnumerable<string>> GetFollowingAsync(string userId);
}
