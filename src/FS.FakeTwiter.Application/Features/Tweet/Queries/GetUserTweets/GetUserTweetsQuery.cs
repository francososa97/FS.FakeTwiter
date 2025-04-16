using MediatR;

namespace FS.FakeTwitter.Application.Features.Tweets.Queries;

public class GetUserTweetsQuery : IRequest<IEnumerable<string>>
{
    public string UserId { get; set; } = string.Empty;
}
