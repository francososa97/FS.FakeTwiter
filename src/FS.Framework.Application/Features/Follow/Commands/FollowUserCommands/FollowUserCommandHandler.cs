using FS.FakeTwitter.Application.Features.Follows.Commands;
using FS.FakeTwitter.Application.Interfaces.Follows;
using MediatR;

public class FollowUserCommandHandler : IRequestHandler<FollowUserCommand, Unit>
{
    private readonly IFollowService _followService;

    public FollowUserCommandHandler(IFollowService followService)
    {
        _followService = followService;
    }

    public async Task<Unit> Handle(FollowUserCommand request, CancellationToken cancellationToken)
    {
        await _followService.FollowAsync(request.FollowerId, request.FolloweeId);
        return Unit.Value;
    }
}
