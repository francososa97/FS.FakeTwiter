using FS.FakeTwiter.Application.DTOs;
using MediatR;

namespace FS.FakeTwiter.Application.Features.Users.Commands.CreateUserCommand;

public record CreateUserCommand(string Username, string Email) : IRequest<OperationResultDto>;
