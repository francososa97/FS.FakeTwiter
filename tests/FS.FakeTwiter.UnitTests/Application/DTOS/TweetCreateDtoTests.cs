using FS.FakeTwitter.Application.DTOs;

namespace FS.FakeTwiter.UnitTests.Application.DTOS;

public class TweetCreateDtoTests
{
    [Fact]
    public void TweetCreateDto_Should_Set_And_Get_Properties_Correctly()
    {
        // Arrange
        var userId = "user-x";
        var content = "Este es un tweet de prueba";

        // Act
        var dto = new TweetCreateDto
        {
            UserId = userId,
            Content = content
        };

        // Assert
        Assert.Equal(userId, dto.UserId);
        Assert.Equal(content, dto.Content);
    }
}
