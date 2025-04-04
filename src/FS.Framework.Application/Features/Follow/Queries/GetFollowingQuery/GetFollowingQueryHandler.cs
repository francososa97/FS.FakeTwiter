using FS.FakeTwitter.Application.Features.Follows.Queries;
using FS.FakeTwitter.Application.Interfaces.Follows;
using MediatR;

public class GetFollowingQueryHandler : IRequestHandler<GetFollowingQuery, IEnumerable<string>>
{
    private readonly IFollowService _followService;

    public GetFollowingQueryHandler(IFollowService followService)
    {
        _followService = followService;
    }

    public async Task<IEnumerable<string>> Handle(GetFollowingQuery request, CancellationToken cancellationToken)
    {
        return await _followService.GetFollowingAsync(request.UserId);
    }
}
