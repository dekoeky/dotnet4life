using WebApplication1.Services;

namespace WebApplication1.Configuration;

/// <summary>
/// Configuration related to application services.
/// </summary>
internal static class Services
{
    /// <summary>
    /// Adds the application specific services to the service collection.
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IWeatherForecastService, RandomWeatherForecastService>();


        return services;
    }
}