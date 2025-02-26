using Microsoft.Extensions.Diagnostics.HealthChecks;
using WebApplication1.Services;

namespace WebApplication1.HealthChecks.Checks;

/// <summary>
/// A Health Check that checks if the initialization process has been completed.
/// </summary>
/// <seealso cref="InitializationService"/>
/// <seealso cref="InitializationState"/>
internal class InitializationHealthCheck(InitializationState state) : IHealthCheck
{
    private static readonly Task<HealthCheckResult> successResult = Task.FromResult(HealthCheckResult.Healthy("Initialized"));

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        if (state.Initialized)
            return successResult;

        //To demonstrate the possibility of adding data to your result
        var data = new Dictionary<string, object>
        {
            { nameof(state.EstimatedTimeOfInitialization), state.EstimatedTimeOfInitialization },
            { nameof(state.PercentageInitialized), state.PercentageInitialized }
        };

        return Task.FromResult(HealthCheckResult.Unhealthy("Not yet Initialized", data: data));
    }
}
