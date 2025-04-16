using FS.FakeTwitter.Application.Exceptions;
using MediatR;
using FS.FakeTwiter.Application.Features.Tweet.Commands.PostTweetCommand;
using Microsoft.Extensions.Logging;

public class PostTweetCommandHandler : IRequestHandler<PostTweetCommand, ApiResponse<Guid>>
{
    private readonly ITweetService _tweetService;
    private readonly IUserService _userService;
    private readonly ILogger<PostTweetCommandHandler> _logger;

    public PostTweetCommandHandler(
        ITweetService tweetService, 
        IUserService userService, 
        ILogger<PostTweetCommandHandler> logger)
    {
        _tweetService = tweetService;
        _userService = userService;
        _logger = logger;
    }

    public async Task<ApiResponse<Guid>> Handle(PostTweetCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(Guid.Parse(request.UserId));

        if (user is null)
            throw new NotFoundException($"El usuario '{request.UserId}' no existe.");

        var result = await _tweetService.PostTweetAsync(user.Id.ToString(), request.Content);

        return new ApiResponse<Guid>(result, "Tweet publicado");
    }
}
