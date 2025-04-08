using FS.FakeTwiter.Application.DTOs;
using FS.FakeTwiter.Application.Interfaces.Users;
using FS.FakeTwiter.Application.Mappers;
using FS.FakeTwitter.Domain.Interfaces;
using MediatR;

namespace FS.FakeTwiter.Application.Features.Users.Commands.CreateUserCommand;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, OperationResultDto>
{
    private readonly IUserService _userService;
    public CreateUserHandler(IUserService userService) => _userService = userService;

    public async Task<OperationResultDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _userService.AddAsync(request.ToEntity());
        return result.ToResultDto("creado");
    }
}
