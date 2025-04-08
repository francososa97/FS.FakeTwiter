using FS.FakeTwitter.Domain.Entities;
using MediatR;

namespace FS.FakeTwiter.Application.Features.Users.Queries.GetAllUsersQuery;

public record GetAllUsersQuery() : IRequest<IEnumerable<User>>;
