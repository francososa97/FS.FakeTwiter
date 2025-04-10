using Xunit;
using global::FS.FakeTwiter.Application.DTOs;


namespace FS.FakeTwiter.UnitTests.Application.DTOS;

public class OperationResultDtoTests
{
    [Fact]
    public void Should_Have_Default_Values()
    {
        // Arrange & Act
        var operationResult = new OperationResultDto();

        // Assert
        Assert.True(operationResult.Success);  // Success debe ser true por defecto
        Assert.Equal(string.Empty, operationResult.Message);  // Message debe ser cadena vacía por defecto
    }

    [Fact]
    public void Should_Set_Values_Correctly()
    {
        // Arrange
        var operationResult = new OperationResultDto
        {
            Success = false,
            Message = "Operation failed."
        };

        // Assert
        Assert.False(operationResult.Success);  // Success debe ser false
        Assert.Equal("Operation failed.", operationResult.Message);  // Message debe ser igual a "Operation failed."
    }
}
