using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Tailoring.Data;
using Tailoring.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode=StatusCodes.Status429TooManyRequests;
    options.AddPolicy("fixed",httpContext =>
    RateLimitPartition.GetFixedWindowLimiter(
        partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
        factory: partition => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 1,
            Window = TimeSpan.FromSeconds(10)
        }));
    
});
var app = builder.Build();

await app.Services.InitializeDbAsync();

app.MapPostEndpoints();
app.MapCommentsEndPoints();
app.MapUsersEndPoints();
app.UseRateLimiter();

app.Run();