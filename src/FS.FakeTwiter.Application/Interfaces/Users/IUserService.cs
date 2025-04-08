using FS.FakeTwitter.Domain.Entities;

namespace FS.FakeTwiter.Application.Interfaces.Users;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task<int> AddAsync(User user);
    Task<User> UpdateAsync(User user);
    Task<int> DeleteAsync(Guid id);
}
