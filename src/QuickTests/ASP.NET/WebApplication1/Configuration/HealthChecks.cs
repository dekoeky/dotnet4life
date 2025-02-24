using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using WebApplication1.HealthChecks;

namespace WebApplication1.Configuration;

internal static class HealthChecks
{
    /// <summary>
    /// Adds this application's specified health checks.
    /// </summary>
    public static IHealthChecksBuilder AddApplicationHealthChecks(this IServiceCollection services)
        => services.AddHealthChecks()
            .AddAlwaysHealthyCheck()
        ;

    public static IEndpointRouteBuilder MapApplicationHealthChecks(this IEndpointRouteBuilder webApplication)
    {
        //Regular plain health check endpoint
        webApplication.MapHealthChecks("health");

        //A health check endpoint that returns ab it more detail regarding the separate results.
        webApplication.MapHealthChecks("health/explain", new HealthCheckOptions
        {
            ResponseWriter = HealthCheckResponseWriters.WriteExplainResponse,
        });

        //TODO: Image healthcheck result writer

        return webApplication;
    }
}