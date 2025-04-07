using System.IO;
using System;
using System.Net;
using System.Threading.Tasks;
using FS.FakeTwitter.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace FS.FakeTwiter.IntegrationTests.Middleware;

public class ErrorHandlerMiddlewareTests
{
    [Fact]
    public async Task Invoke_WhenAppExceptionThrown_ShouldReturnCustomStatusCode()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();
        var middleware = new ErrorHandlerMiddleware(_ => throw new AppException("Mensaje de error", ((int)HttpStatusCode.BadRequest)));

        // Act
        await middleware.Invoke(context);

        // Assert
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(context.Response.Body);
        var body = await reader.ReadToEndAsync();

        Assert.Equal((int)HttpStatusCode.BadRequest, context.Response.StatusCode);
        Assert.Contains("Mensaje de error", body);
    }


    [Fact]
    public async Task Invoke_WhenUnhandledExceptionThrown_ShouldReturnInternalServerError()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream(); // <- importante
        var middleware = new ErrorHandlerMiddleware(_ => throw new Exception("Unhandled"));

        // Act
        await middleware.Invoke(context);

        // Assert
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(context.Response.Body);
        var body = await reader.ReadToEndAsync();

        Assert.Equal((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);
        Assert.Contains("Internal Server Error", body);
        Assert.Contains("Unhandled", body);
    }

}
