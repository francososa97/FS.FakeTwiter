using FS.FakeTwiter.Application.DTOs;
using FS.FakeTwiter.Application.Mappers;
using MediatR;

namespace FS.FakeTwiter.Application.Features.Users.Queries.GetUserByIdQuery;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, ApiResponse<UserDto>>
{
    private readonly IUserService _userService;
    public GetUserByIdHandler(IUserService userService) => _userService = userService;

    public async Task<ApiResponse<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(request.Id);
        return user is null
            ? new ApiResponse<UserDto>(false, "Usuario no encontrado.")
            : new ApiResponse<UserDto>(user.ToDto());
    }
}
