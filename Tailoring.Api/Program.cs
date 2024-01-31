using Tailoring.Data;
using Tailoring.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRepositories(builder.Configuration);
var app = builder.Build();

await app.Services.InitializeDbAsync();

app.MapPostEndpoints();
app.MapCommentsEndPoints();
app.MapUsersEndPoints();

app.Run();