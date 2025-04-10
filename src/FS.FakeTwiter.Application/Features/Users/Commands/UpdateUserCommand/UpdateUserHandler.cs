using FS.FakeTwiter.Application.DTOs;
using FS.FakeTwiter.Application.Mappers;
using MediatR;

namespace FS.FakeTwiter.Application.Features.Users.Commands.UpdateUserCommand;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, ApiResponse<UserDto>>
{
    private readonly IUserService _userService;
    public UpdateUserHandler(IUserService userService) => _userService = userService;

    public async Task<ApiResponse<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var response = await _userService.UpdateAsync(request.ToEntity());
        return new ApiResponse<UserDto>(response.ToDto(), "Usuario actualizado correctamente.");
    }
}
