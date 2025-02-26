using WebApplication1.Configuration;
using WebApplication1.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
//Add the required HealthChecks (and dependencies) to the app's Dependency Injection container.
builder.Services.AddApplicationHealthChecks();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
//Register our minimal api endpoints (The core of our example asp.NET app)
app.MapWeatherForecastEndpoint();
//Register our health check endpoints (a.k.a. map them to a path)
app.MapApplicationHealthChecks();

app.Run();