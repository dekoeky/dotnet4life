using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WebApplication1.HealthChecks.Checks;

/// <summary>
/// A health check that always returns a <see cref="HealthStatus.Healthy"/> result.
/// </summary>
/// <remarks>
/// Apart from demonstrating the possibilities of HealthChecks, this class does not serve much purpose.
/// </remarks>
internal class AlwaysHealthyHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        => Task.FromResult(HealthCheckResult.Healthy("Always Healthy"));
}