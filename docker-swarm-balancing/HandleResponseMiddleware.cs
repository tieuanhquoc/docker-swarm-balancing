namespace docker_swarm_balancing;

public class HandleResponseMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public HandleResponseMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger<HandleResponseMiddleware>();
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
            _logger.LogInformation(
                "Http Response Information | Method: {Method} | Path: {Path} | Status Code: {StatusCode}",
                context.Request.Method.ToUpper(),
                context.Request.Path,
                context.Response.StatusCode
            );
        }
        catch (Exception exception)
        {
            _logger.LogError(
                "Http Response Error | Method: {Method} | Path: {Path} | Status Code: {StatusCode} | Message: {Message}",
                context.Request.Method.ToUpper(),
                context.Request.Path,
                context.Response.StatusCode,
                exception.Message
            );
        }
    }
}