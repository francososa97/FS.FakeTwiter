using FS.FakeTwitter.Application.Features.Tweets.Queries;
using MediatR;

public class GetTimelineQueryHandler : IRequestHandler<GetTimelineQuery, IEnumerable<string>>
{
    private readonly ITweetService _tweetService;

    public GetTimelineQueryHandler(ITweetService tweetService)
    {
        _tweetService = tweetService;
    }

    public async Task<IEnumerable<string>> Handle(GetTimelineQuery request, CancellationToken cancellationToken)
    {
        return await _tweetService.GetTimelineAsync(request.UserId);
    }
}
