using FluentValidation;

namespace FS.FakeTwiter.Application.Features.Users.Commands.CreateUserCommand;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("El nombre de usuario es requerido.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("El correo electrónico no es válido.");
    }
}
