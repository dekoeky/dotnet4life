using WebApplication1.HealthChecks.Checks;

namespace WebApplication1.Services;

/// <summary>
/// Hols the state of a simulated initialization service.
/// </summary>
/// <seealso cref="InitializationService"/>
/// <seealso cref="InitializationHealthCheck"/>
internal class InitializationState
{
    public bool Initialized { get; internal set; }

    /// <summary>
    /// How many percent is the initialization finished.
    /// </summary>
    /// <remarks>
    /// 1.0 == 100%
    /// </remarks>
    public double PercentageInitialized { get; internal set; }

    public DateTime EstimatedTimeOfInitialization { get; internal set; }
}
