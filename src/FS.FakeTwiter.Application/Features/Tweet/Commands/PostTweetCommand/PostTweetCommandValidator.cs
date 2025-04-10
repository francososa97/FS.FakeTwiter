using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FS.FakeTwiter.Application.Features.Tweet.Commands.PostTweetCommand;

public class PostTweetCommandValidator : AbstractValidator<PostTweetCommand>
{
    public PostTweetCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("El ID de usuario es requerido.")
            .Must(BeValidGuid).WithMessage("El formato del ID de usuario es inválido.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("El contenido no puede estar vacío.")
            .MaximumLength(280).WithMessage("El tweet no puede superar los 280 caracteres.");
    }

    private bool BeValidGuid(string userId)
        => Guid.TryParse(userId, out _);
}
