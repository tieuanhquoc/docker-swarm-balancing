using docker_swarm_balancing;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(logBuilder =>
{
    logBuilder.ClearProviders();
    logBuilder.AddConsole();
    logBuilder.AddTraceSource("Information, ActivityTracing");
});

var app = builder.Build();
app.MapGet("/", async context =>
{
    var result = new
    {
        RequestId = Guid.NewGuid(),
        Message = "Thành công",
        RequestAt = DateTime.UtcNow.AddHours(7)
    };

    await context.Response.WriteAsJsonAsync(result);
});
app.UseMiddleware<HandleResponseMiddleware>();
app.Run();