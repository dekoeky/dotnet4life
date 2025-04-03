using Formatters.Models;
using Formatters.Services.WeatherForecasts;
using Microsoft.AspNetCore.Mvc;

namespace Formatters.Handlers;

public class WeatherHandlers
{
    public static IEnumerable<WeatherForecast> GetWeatherForecast(IWeatherForecastService weatherForecastService)
    {
        return weatherForecastService.WeatherForecasts();
    }

    public static void PostWeatherForecast(
        [FromServices] ILogger<WeatherHandlers> logger,
        [FromBody] IEnumerable<WeatherForecast> forecast)
    {
        using var loggerScope = logger.BeginScope("Test");

        foreach (var item in forecast)
            logger.LogInformation("{WeatherForecastSummary}", item.Summary);
    }
}