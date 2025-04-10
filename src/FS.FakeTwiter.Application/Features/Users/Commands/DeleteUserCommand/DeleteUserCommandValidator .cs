using FluentValidation;

namespace FS.FakeTwiter.Application.Features.Users.Commands.DeleteUserCommand;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID del usuario es obligatorio.");
    }
}
