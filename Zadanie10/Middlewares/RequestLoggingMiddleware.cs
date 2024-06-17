using System.Diagnostics;

namespace Zadanie10.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        LogRequest(context);
        await _next(context);

        stopwatch.Stop();
        LogResponse(context, stopwatch.ElapsedMilliseconds);
    }

    private void LogRequest(HttpContext context)
    {
        var request = context.Request;
        Debug.WriteLine($"Incoming Request: {request.Method} {request.Path} at {DateTime.Now}");
        Debug.WriteLine($"Headers: {string.Join(", ", request.Headers)}");
    }

    private void LogResponse(HttpContext context, long processingTime)
    {
        var response = context.Response;
        Debug.WriteLine($"Outgoing Response: {response.StatusCode} processed in {processingTime}ms at {DateTime.Now}");
        Debug.WriteLine($"Response Headers: {string.Join(", ", response.Headers)}");
    }
}