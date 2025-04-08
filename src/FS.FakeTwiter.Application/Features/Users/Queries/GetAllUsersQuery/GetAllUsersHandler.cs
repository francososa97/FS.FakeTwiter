using FS.FakeTwiter.Application.Interfaces.Users;
using FS.FakeTwitter.Domain.Entities;
using MediatR;

namespace FS.FakeTwiter.Application.Features.Users.Queries.GetAllUsersQuery;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
{
    private readonly IUserService _userService;
    public GetAllUsersHandler(IUserService userService) => _userService = userService;

    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        => await _userService.GetAllAsync();
}
