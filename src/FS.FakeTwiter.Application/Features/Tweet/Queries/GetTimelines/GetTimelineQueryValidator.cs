using FluentValidation;
using FS.FakeTwitter.Application.Features.Tweets.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FS.FakeTwiter.Application.Features.Tweet.Queries.GetTimelines;
public class GetTimelineQueryValidator : AbstractValidator<GetTimelineQuery>
{
    public GetTimelineQueryValidator()
    {
        RuleFor(x => x.UserId)
        .NotEmpty().WithMessage("El ID de usuario no puede estar vacío.")
        .Must(id => Guid.TryParse(id, out _)).WithMessage("El ID de usuario debe ser un GUID válido.");

    }
}