using Moq;
using global::FS.FakeTwiter.Application.Features.Users.Commands.CreateUserCommand;
using FS.FakeTwitter.Domain.Entities;

namespace FS.FakeTwiter.UnitTests.Handlers.CreateUserCommandTests;

public class CreateUserCommandHandlerTests
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly CreateUserHandler _handler;

    public CreateUserCommandHandlerTests()
    {
        _userServiceMock = new Mock<IUserService>();
        _handler = new CreateUserHandler(_userServiceMock.Object);
    }

    [Fact]
    public async Task Should_Return_Error_When_Email_Exists()
    {
        // Arrange
        var command = new CreateUserCommand("existing@test.com", "NewUser");
        _userServiceMock.Setup(s => s.EmailExistsAsync(command.Email)).ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Ya existe un usuario con ese email.", result.Message);
    }

    [Fact]
    public async Task Should_Return_Error_When_User_Creation_Fails()
    {
        // Arrange
        var command = new CreateUserCommand("newuser@test.com", "NewUser");
        _userServiceMock.Setup(s => s.EmailExistsAsync(command.Email)).ReturnsAsync(false);
        _userServiceMock.Setup(s => s.AddAsync(It.IsAny<User>())).ReturnsAsync((User)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Error al crear el usuario.", result.Message);
    }
}