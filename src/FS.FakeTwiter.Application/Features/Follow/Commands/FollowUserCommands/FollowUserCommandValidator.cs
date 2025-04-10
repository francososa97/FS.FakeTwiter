using FluentValidation;
using FS.FakeTwitter.Application.Features.Follows.Commands;

namespace FS.FakeTwiter.Application.Features.Follow.Commands.FollowUserCommands;

public class FollowUserCommandValidator : AbstractValidator<FollowUserCommand>
{
    public FollowUserCommandValidator()
    {
        RuleFor(x => x.FollowerId)
            .NotEmpty().WithMessage("El ID del seguidor es obligatorio.")
            .NotEqual(x => x.FollowId).WithMessage("No podés seguirte a vos mismo.");
        RuleFor(x => x.FollowId)
            .NotEmpty().WithMessage("El ID del seguido es obligatorio.");
    }
}
