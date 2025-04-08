using FS.FakeTwiter.Application.DTOs;
using FS.FakeTwiter.Application.Interfaces.Users;
using FS.FakeTwiter.Application.Mappers;
using FS.FakeTwitter.Domain.Entities;
using MediatR;

namespace FS.FakeTwiter.Application.Features.Users.Commands.UpdateUserCommand;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserDto>
{
    private readonly IUserService _userService;
    public UpdateUserHandler(IUserService userService) => _userService = userService;

    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        //To do Refactor This
        var response = await _userService.UpdateAsync(request.ToEntity());
        return response.ToDto();
    }
}
