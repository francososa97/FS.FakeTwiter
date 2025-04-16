using System.Diagnostics;

namespace FS.FakeTwiter.Api.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var request = context.Request;
        var correlationId = context.Request.Headers["X-Correlation-ID"].FirstOrDefault()
                            ?? context.Items["X-Correlation-ID"]?.ToString();

        try
        {
            await _next(context);

            stopwatch.Stop();

            _logger.LogInformation("➡️ {Method} {Path} responded {StatusCode} in {ElapsedMilliseconds}ms [CorrelationId: {CorrelationId}]",
                request.Method,
                request.Path,
                context.Response.StatusCode,
                stopwatch.ElapsedMilliseconds,
                correlationId);
        }
        catch (Exception ex)
        {
            stopwatch.Stop();

            _logger.LogError(ex, "❌ {Method} {Path} threw an error after {ElapsedMilliseconds}ms [CorrelationId: {CorrelationId}]",
                request.Method,
                request.Path,
                stopwatch.ElapsedMilliseconds,
                correlationId);

            throw;
        }
    }
}
