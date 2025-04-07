using FS.FakeTwitter.Application.Exceptions;

namespace FS.FakeTwitter.UnitTests.Exceptions;

public class UnauthorizedExceptionTests
{
    [Fact]
    public void Constructor_ShouldSetDefaultMessageAndStatusCode()
    {
        // Arrange & Act
        var exception = new UnauthorizedException();

        // Assert
        Assert.Equal("Unauthorized access.", exception.Message);
        Assert.Equal(401, exception.StatusCode);
    }

    [Fact]
    public void Constructor_WithCustomMessage_ShouldSetMessageAndStatusCode()
    {
        // Arrange
        var message = "Custom unauthorized message";

        // Act
        var exception = new UnauthorizedException(message);

        // Assert
        Assert.Equal(message, exception.Message);
        Assert.Equal(401, exception.StatusCode);
    }
}
