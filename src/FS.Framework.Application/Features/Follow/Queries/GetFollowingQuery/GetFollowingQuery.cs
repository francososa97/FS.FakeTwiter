﻿using MediatR;

namespace FS.FakeTwitter.Application.Features.Follows.Queries;

public class GetFollowingQuery : IRequest<IEnumerable<string>>
{
    public string UserId { get; set; } = string.Empty;
}
