using FS.FakeTwitter.Domain.Entities;
using MediatR;

namespace FS.FakeTwiter.Application.Features.Users.Queries.GetUserByIdQuery;

public record GetUserByIdQuery(Guid Id) : IRequest<User?>;
