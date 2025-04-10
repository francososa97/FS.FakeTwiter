using FS.FakeTwiter.Application.DTOs;
using FS.FakeTwiter.Application.Features.Users.Commands.CreateUserCommand;
using FS.FakeTwiter.Application.Features.Users.Commands.UpdateUserCommand;
using FS.FakeTwitter.Domain.Entities;

namespace FS.FakeTwiter.Application.Mappers;

public static class UserMapper
{
    public static UserDto ToDto(this User user) => new UserDto
    {
        Id = user.Id,
        Username = user.Username,
        Email = user.Email
    };

    public static User ToEntity(this CreateUserCommand cmd)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Username = cmd.Username,
            Email = cmd.Email
        };
    }
    public static User ToEntity(this UpdateUserCommand cmd)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Username = cmd.Username,
            Email = cmd.Email
        };
    }
}
