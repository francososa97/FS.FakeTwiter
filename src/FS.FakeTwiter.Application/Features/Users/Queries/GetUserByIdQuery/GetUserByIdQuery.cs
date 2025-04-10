using FS.FakeTwiter.Application.DTOs;
using MediatR;

namespace FS.FakeTwiter.Application.Features.Users.Queries.GetUserByIdQuery;

public record GetUserByIdQuery(Guid Id) : IRequest<ApiResponse<UserDto>>;
