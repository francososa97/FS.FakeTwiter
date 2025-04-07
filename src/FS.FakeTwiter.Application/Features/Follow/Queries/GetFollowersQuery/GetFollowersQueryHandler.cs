using FS.FakeTwitter.Application.Features.Follows.Queries;
using FS.FakeTwitter.Application.Interfaces.Follows;
using MediatR;

public class GetFollowersQueryHandler : IRequestHandler<GetFollowersQuery, IEnumerable<string>>
{
    private readonly IFollowService _followService;

    public GetFollowersQueryHandler(IFollowService followService)
    {
        _followService = followService;
    }

    public async Task<IEnumerable<string>> Handle(GetFollowersQuery request, CancellationToken cancellationToken)
    {
        return await _followService.GetFollowersAsync(request.UserId);
    }
}
