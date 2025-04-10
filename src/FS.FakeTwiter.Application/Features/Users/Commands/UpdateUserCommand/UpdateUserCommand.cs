using FS.FakeTwiter.Application.DTOs;
using MediatR;

namespace FS.FakeTwiter.Application.Features.Users.Commands.UpdateUserCommand;
public record UpdateUserCommand(Guid Id, string Username, string Email) : IRequest<ApiResponse<UserDto>>;
