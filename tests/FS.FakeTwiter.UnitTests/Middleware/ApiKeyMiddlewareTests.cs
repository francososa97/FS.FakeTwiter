using System.Text;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using FS.FakeTwiter.Api.Middleware;

namespace FS.FakeTwiter.UnitTests.Middleware;

public class ApiKeyMiddlewareTests
{
    private readonly Mock<RequestDelegate> _nextMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly ApiKeyMiddleware _middleware;
    private readonly DefaultHttpContext _httpContext;

    public ApiKeyMiddlewareTests()
    {
        _nextMock = new Mock<RequestDelegate>();
        _configurationMock = new Mock<IConfiguration>();
        _httpContext = new DefaultHttpContext();
        _middleware = new ApiKeyMiddleware(_nextMock.Object);
    }

    [Fact]
    public async Task Should_Return_401_When_No_ApiKey_Provided()
    {
        _httpContext.Request.Headers.Clear();
        await _middleware.InvokeAsync(_httpContext, _configurationMock.Object);

        Assert.Equal(StatusCodes.Status401Unauthorized, _httpContext.Response.StatusCode);
        using (var memoryStream = new MemoryStream())
        {
            await _httpContext.Response.Body.CopyToAsync(memoryStream);
            var responseBody = Encoding.UTF8.GetString(memoryStream.ToArray());


            Assert.Equal(StatusCodes.Status401Unauthorized, _httpContext.Response.StatusCode);
        }
    }

    [Fact]
    public async Task Should_Skip_Swagger_Route()
    {
        _httpContext.Request.Path = "/swagger";

        await _middleware.InvokeAsync(_httpContext, _configurationMock.Object);

        _nextMock.Verify(next => next.Invoke(It.IsAny<HttpContext>()), Times.Once);
    }
}
