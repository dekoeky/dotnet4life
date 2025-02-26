using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WebApplication1.Services;

namespace WebApplication1.HealthChecks.Checks;

/// <summary>
/// Dependency Injection for <see cref="InitializationHealthCheck"/>
/// </summary>
internal static class InitializationHealthCheckDependencyInjection
{
    public static void AddInitializationCheck(this IHealthChecksBuilder hc)
    {
        hc.AddCheck<InitializationHealthCheck>("Initialization", HealthStatus.Degraded, [Tags.Ready]);

        //If not yet added, add the State
        hc.Services.TryAddSingleton<InitializationState>();

        //If not yet added, add the Service
        hc.Services.TryAddSingleton<InitializationService>();
        hc.Services.AddHostedService(f => f.GetRequiredService<InitializationService>());
    }
}