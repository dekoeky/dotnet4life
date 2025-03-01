using WebApplication1.Models;

namespace WebApplication1.Endpoints;

/// <summary>
/// Contains Weather Related (Minimal Api) Endpoints
/// </summary>
public static class WeatherEndpoints
{
    private static readonly string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

    public static void MapWeatherForecastEndpoint(this WebApplication app)
    {
        app.MapGet("/weatherforecast", static () => GetWeatherForecasts())
            .WithName("GetWeatherForecast");
    }

    private static WeatherForecast[] GetWeatherForecasts() => Enumerable.Range(1, 5)
        .Select(index => new WeatherForecast(
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
            )).ToArray();
}