using FluentValidation;
using FS.FakeTwitter.Application.Features.Tweets.Queries;

namespace FS.FakeTwiter.Application.Features.Tweet.Queries.GetUserTweets;
public class GetUserTweetsQueryValidator : AbstractValidator<GetUserTweetsQuery>
{
    public GetUserTweetsQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("El ID de usuario no puede estar vacío.")
            .Must(id => Guid.TryParse(id, out _)).WithMessage("El ID de usuario debe ser un GUID válido.");
    }
}

 