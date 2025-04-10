using FS.FakeTwiter.Infrastructure.DataAccess;
using FS.FakeTwitter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FS.FakeTwitter.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync() =>
               await _context.Users.Where(u => !u.IsDeleted).ToListAsync();

        public async Task<User?> GetByIdAsync(Guid id) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public User Update(User user)
        {
            _context.Users.Update(user);
            return user;
        }
        public async Task DeleteAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user is not null)
            {
                user.IsDeleted = true;
                _context.Users.Update(user);
            }
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email && !u.IsDeleted);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}