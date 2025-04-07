using FS.FakeTwitter.Application.Exceptions;

namespace FS.FakeTwiter.UnitTests.Application.Exceptions;

public class AppExceptionTests
{
    [Fact]
    public void Constructor_Should_Set_Message_And_StatusCode()
    {
        // Arrange
        var expectedMessage = "Algo salió mal";
        var expectedStatusCode = 422;

        // Act
        var exception = new AppException(expectedMessage, expectedStatusCode);

        // Assert
        Assert.Equal(expectedMessage, exception.Message);
        Assert.Equal(expectedStatusCode, exception.StatusCode);
    }

    [Fact]
    public void Constructor_Should_Default_StatusCode_To_400()
    {
        // Arrange
        var expectedMessage = "Error por defecto";

        // Act
        var exception = new AppException(expectedMessage);

        // Assert
        Assert.Equal(expectedMessage, exception.Message);
        Assert.Equal(400, exception.StatusCode);
    }
}
