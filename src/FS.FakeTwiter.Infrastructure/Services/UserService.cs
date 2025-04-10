using FS.FakeTwiter.Application.Interfaces.Users;
using FS.FakeTwitter.Application.Exceptions;
using FS.FakeTwitter.Domain.Entities;
using FS.FakeTwitter.Domain.Interfaces;

namespace FS.FakeTwitter.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<User>> GetAllAsync() =>
        (await _unitOfWork.Users.GetAllAsync());

    public async Task<User?> GetByIdAsync(Guid id) =>
        (await _unitOfWork.Users.GetByIdAsync(id));

    public async Task<int> AddAsync(User user)
    {
        await _unitOfWork.Users.AddAsync(user);
        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<User> UpdateAsync(User user)
    {
        var existingUser = await _unitOfWork.Users.GetByIdAsync(user.Id);
        if (existingUser is null)
            throw new NotFoundException("Usuario no encontrado.");

        existingUser.Username = user.Username;
        existingUser.Email = user.Email;

        var updated = _unitOfWork.Users.Update(existingUser);
        await _unitOfWork.SaveChangesAsync();
        return updated;
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        await _unitOfWork.Users.DeleteAsync(id);
        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _unitOfWork.Users.EmailExistsAsync(email);
    }

}