using FluentValidation;

namespace FS.FakeTwiter.Application.Features.Users.Commands.UpdateUserCommand;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID es obligatorio.");
        RuleFor(x => x.Username).NotEmpty().WithMessage("El nombre de usuario es requerido.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("El correo electrónico no es válido.");
    }
}
