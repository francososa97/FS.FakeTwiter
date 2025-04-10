using MediatR;
namespace FS.FakeTwiter.Application.Features.Users.Commands.DeleteUserCommand;

public record DeleteUserCommand(Guid Id) : IRequest<ApiResponse>;
