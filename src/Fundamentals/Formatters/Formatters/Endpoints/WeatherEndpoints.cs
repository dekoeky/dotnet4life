using Formatters.Handlers;

namespace Formatters.Endpoints;

public static class WeatherEndpoints
{
    public static void MapWeatherForecastEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("WeatherForecast", WeatherHandlers.GetWeatherForecast);
        endpoints.MapPost("WeatherForecast", WeatherHandlers.PostWeatherForecast);
    }
}