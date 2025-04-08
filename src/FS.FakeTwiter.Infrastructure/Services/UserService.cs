using FS.FakeTwiter.Application.Interfaces.Users;
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
        var response = _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync();
        return response;
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        await _unitOfWork.Users.DeleteAsync(id);
        return await _unitOfWork.SaveChangesAsync();
    }
}