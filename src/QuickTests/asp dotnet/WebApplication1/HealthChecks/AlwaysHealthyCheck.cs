using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WebApplication1.HealthChecks;

internal static class AlwaysHealthyCheck
{
    /// <summary>
    /// Adds a health check that always returns healthy.
    /// </summary>
    /// <param name="healthChecksBuilder"></param>
    /// <returns></returns>
    public static IHealthChecksBuilder AddAlwaysHealthyCheck(this IHealthChecksBuilder healthChecksBuilder) =>
        healthChecksBuilder
            .AddCheck("always-healthy", () => HealthCheckResult.Healthy("I'm always healthy!"), [Tags.Demo]);
}