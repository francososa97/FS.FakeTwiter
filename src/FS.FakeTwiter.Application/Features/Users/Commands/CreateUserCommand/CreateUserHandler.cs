using FS.FakeTwiter.Application.DTOs;
using FS.FakeTwiter.Application.Mappers;
using MediatR;

namespace FS.FakeTwiter.Application.Features.Users.Commands.CreateUserCommand;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, ApiResponse<UserDto>>
{
    private readonly IUserService _userService;
    public CreateUserHandler(IUserService userService) => _userService = userService;

    public async Task<ApiResponse<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var emailExists = await _userService.EmailExistsAsync(request.Email);

        if (emailExists)
            return new ApiResponse<UserDto>(new UserDto(),"Ya existe un usuario con ese email.") { Success = false };

        var user = request.ToEntity();
        var newUser = await _userService.AddAsync(user);

        if (newUser != null)
        {
            return new ApiResponse<UserDto>(newUser.ToDto(), "Usuario creado exitosamente.");
        }

        return new ApiResponse<UserDto>(new UserDto(), "Error al crear el usuario.") { Success = false };
    }
}
