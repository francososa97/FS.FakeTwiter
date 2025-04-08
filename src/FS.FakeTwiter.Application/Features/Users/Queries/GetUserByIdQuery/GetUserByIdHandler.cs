using FS.FakeTwiter.Application.Interfaces.Users;
using FS.FakeTwitter.Domain.Entities;
using MediatR;

namespace FS.FakeTwiter.Application.Features.Users.Queries.GetUserByIdQuery;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, User?>
{
    private readonly IUserService _userService;
    public GetUserByIdHandler(IUserService userService) => _userService = userService;

    public async Task<User?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        => await _userService.GetByIdAsync(request.Id);
}
