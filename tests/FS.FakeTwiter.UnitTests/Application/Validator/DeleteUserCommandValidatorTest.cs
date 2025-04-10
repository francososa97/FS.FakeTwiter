using FluentValidation.TestHelper;
using global::FS.FakeTwiter.Application.Features.Users.Commands.DeleteUserCommand;
using Xunit;

namespace FS.FakeTwiter.UnitTests.Application.Validator;
public class DeleteUserCommandValidatorTests
{
    private readonly DeleteUserCommandValidator _validator;

    public DeleteUserCommandValidatorTests()
    {
        _validator = new DeleteUserCommandValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Id_Is_Empty()
    {
        // Arrange
        var command = new DeleteUserCommand(Id: Guid.Empty);

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Id)
                .WithErrorMessage("El ID del usuario es obligatorio.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Id_Is_Provided()
    {
        // Arrange
        var command = new DeleteUserCommand(Id: Guid.Empty);

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Id)
         .WithErrorMessage("El ID del usuario es obligatorio.");
    }
}

