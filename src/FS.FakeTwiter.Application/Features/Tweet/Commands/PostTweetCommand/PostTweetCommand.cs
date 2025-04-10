using MediatR;

namespace FS.FakeTwiter.Application.Features.Tweet.Commands.PostTweetCommand;

public class PostTweetCommand : IRequest<ApiResponse<Guid>>
{
    public string UserId { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}
