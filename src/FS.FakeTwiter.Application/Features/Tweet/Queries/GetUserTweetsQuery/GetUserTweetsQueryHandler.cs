using FS.FakeTwiter.Application.Interfaces.Tweets;
using FS.FakeTwitter.Application.Features.Tweets.Queries;
using MediatR;

public class GetUserTweetsQueryHandler : IRequestHandler<GetUserTweetsQuery, IEnumerable<string>>
{
    private readonly ITweetService _tweetService;

    public GetUserTweetsQueryHandler(ITweetService tweetService)
    {
        _tweetService = tweetService;
    }

    public async Task<IEnumerable<string>> Handle(GetUserTweetsQuery request, CancellationToken cancellationToken)
    {
        return await _tweetService.GetUserTweetsAsync(request.UserId);
    }
}
