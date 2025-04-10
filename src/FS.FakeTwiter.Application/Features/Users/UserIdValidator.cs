using FluentValidation;

namespace FS.FakeTwiter.Application.Features.Users;

public class UserIdValidator : AbstractValidator<string>
{
    public UserIdValidator()
    {
        RuleFor(x => x)
            .NotEmpty().WithMessage("El ID de usuario es obligatorio.")
            .Must(x => Guid.TryParse(x, out _)).WithMessage("El ID de usuario no tiene formato válido.");
    }
}
