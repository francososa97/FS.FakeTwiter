using MediatR;

namespace FS.FakeTwitter.Application.Features.Follows.Commands;

public class FollowUserCommand : IRequest<Unit> 
{
    public string FollowerId { get; set; } = string.Empty;
    public string FollowId { get; set; } = string.Empty;
}
