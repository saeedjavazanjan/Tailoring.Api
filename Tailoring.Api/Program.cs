using Tailoring.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapPostEndpoints();

app.Run();