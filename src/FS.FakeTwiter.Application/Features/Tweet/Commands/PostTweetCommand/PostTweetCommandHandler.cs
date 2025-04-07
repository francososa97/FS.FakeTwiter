using FS.FakeTwitter.Application.Exceptions;
using MediatR;
using FS.FakeTwiter.Application.Interfaces.Tweets;
using FS.FakeTwiter.Application.Features.Tweet.Commands.PostTweetCommand;

public class PostTweetCommandHandler : IRequestHandler<PostTweetCommand, Guid>
{
    private readonly ITweetService _tweetService;

    public PostTweetCommandHandler(ITweetService tweetService)
    {
        _tweetService = tweetService;
    }

    public async Task<Guid> Handle(PostTweetCommand request, CancellationToken cancellationToken)
    {
        if (request.Content.Length > 280)
            throw new ValidationException("El tweet no puede superar los 280 caracteres.");

        return await _tweetService.PostTweetAsync(request.UserId, request.Content);
    }
}
