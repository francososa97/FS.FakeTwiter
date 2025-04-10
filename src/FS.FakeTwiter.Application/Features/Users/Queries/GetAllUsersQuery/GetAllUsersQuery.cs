using FS.FakeTwiter.Application.DTOs;
using MediatR;

namespace FS.FakeTwiter.Application.Features.Users.Queries.GetAllUsersQuery;

public record GetAllUsersQuery() : IRequest<ApiResponse<IEnumerable<UserDto>>>;
