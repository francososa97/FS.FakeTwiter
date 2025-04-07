using FS.FakeTwitter.Application.DTOs;

namespace FS.FakeTwiter.UnitTests.Application.DTOS;

public class FollowDtoTests
{
    [Fact]
    public void FollowDto_Should_Set_And_Get_Properties_Correctly()
    {
        // Arrange
        var followerId = "user-a";
        var followeeId = "user-b";

        // Act
        var dto = new FollowDto
        {
            FollowerId = followerId,
            FolloweeId = followeeId
        };

        // Assert
        Assert.Equal(followerId, dto.FollowerId);
        Assert.Equal(followeeId, dto.FolloweeId);
    }
}
