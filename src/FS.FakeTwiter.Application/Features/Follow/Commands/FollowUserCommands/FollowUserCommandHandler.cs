using FS.FakeTwitter.Application.Exceptions;
using FS.FakeTwitter.Application.Features.Follows.Commands;
using MediatR;

public class FollowUserCommandHandler : IRequestHandler<FollowUserCommand, Unit>
{
    private readonly IFollowService _followService;
    private readonly IUserService _userService;

    public FollowUserCommandHandler(IFollowService followService, IUserService userService)
    {
        _followService = followService;
        _userService = userService;
    }

    public async Task<Unit> Handle(FollowUserCommand request, CancellationToken cancellationToken)
    {

        var alreadyFollows = await _followService.ExistsAsync(request.FollowerId, request.FollowId);
        if (alreadyFollows)
        {
            var follower = await _userService.GetByIdAsync(Guid.Parse(request.FollowerId));
            var followee = await _userService.GetByIdAsync(Guid.Parse(request.FollowId));

            throw new ValidationException($"El usuario '{follower?.Username}' ya sigue a '{followee?.Username}'.");
        }

        var fromUser = await _userService.GetByIdAsync(Guid.Parse(request.FollowerId));
            var toUser = await _userService.GetByIdAsync(Guid.Parse(request.FollowId));

            if (fromUser is null)
                throw new NotFoundException($"El usuario origen '{request.FollowerId}' no existe.");

            if (toUser is null)
                throw new NotFoundException($"El usuario destino '{request.FollowId}' no existe.");

            await _followService.FollowAsync(request.FollowerId, request.FollowId);
            return Unit.Value;
    }
}
