using WebApplication1.Services;

namespace WebApplication1.MinimalApis;

internal static class WeatherForecastEndpoints
{
    public static void MapWeatherForecastEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("WeatherForecast", (IWeatherForecastService weatherForecastService) => weatherForecastService.WeatherForecasts());
    }
}