namespace Formatters.Endpoints;

public static class ApplicationEndpoints
{
    public static void MapMinimalApis(this WebApplication app)
    {
        var endpoints = app.MapGroup("MinimalApis");

        endpoints.MapWeatherForecastEndpoints();
    }
}