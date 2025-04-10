using FS.FakeTwitter.Domain.Entities;

namespace FS.FakeTwitter.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task AddAsync(User user);
        User Update(User user);
        Task SaveChangesAsync();
        Task DeleteAsync(Guid id);
        Task<bool> EmailExistsAsync(string email);
    }
}