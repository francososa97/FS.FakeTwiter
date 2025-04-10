using FS.FakeTwiter.Application.DTOs;
using FS.FakeTwiter.Application.Interfaces.Users;
using FS.FakeTwiter.Application.Mappers;
using MediatR;

namespace FS.FakeTwiter.Application.Features.Users.Queries.GetAllUsersQuery;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, ApiResponse<IEnumerable<UserDto>>>
{
    private readonly IUserService _userService;
    public GetAllUsersHandler(IUserService userService) => _userService = userService;

    public async Task<ApiResponse<IEnumerable<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userService.GetAllAsync();
        return new ApiResponse<IEnumerable<UserDto>>(users.Select(u => u.ToDto()));
    }
}
