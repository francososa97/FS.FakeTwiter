using FS.FakeTwiter.Application.Mappers;
using MediatR;

namespace FS.FakeTwiter.Application.Features.Users.Commands.CreateUserCommand;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, ApiResponse<string>>
{
    private readonly IUserService _userService;
    public CreateUserHandler(IUserService userService) => _userService = userService;

    public async Task<ApiResponse<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var emailExists = await _userService.EmailExistsAsync(request.Email);

        if (emailExists)
            return new ApiResponse<string>("Ya existe un usuario con ese email.") { Success = false };

        var operation = await _userService.AddAsync(request.ToEntity());
        return new ApiResponse<string>(operation == 1, "Usuario creado exitosamente.");
    }
}
