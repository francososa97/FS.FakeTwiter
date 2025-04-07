using FS.FakeTwitter.Application.Exceptions;

namespace FS.FakeTwitter.UnitTests.Exceptions;

public class NotFoundExceptionTests
{
    [Fact]
    public void Constructor_ShouldSetCorrectMessageAndStatusCode()
    {
        // Arrange
        var entity = "Tweet";
        var key = 123;

        // Act
        var exception = new NotFoundException(entity, key);

        // Assert
        Assert.Equal("Tweet with key '123' was not found.", exception.Message);
        Assert.Equal(404, exception.StatusCode);
    }
}
