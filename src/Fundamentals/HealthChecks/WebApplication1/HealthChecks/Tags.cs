namespace WebApplication1.HealthChecks;

/// <summary>
/// Well-known <see cref="HealthCheckRegistration.Tags"/> values.
/// </summary>
public static class Tags
{
    /// <summary>
    /// Tag to indicate that a health check contributes to the readiness of the service.
    /// </summary>
    public const string Ready = "ready";

    /// <summary>
    /// Tag to indicate that a health check contributes to the fact if the service is running or not.
    /// </summary>
    public const string Live = "live";
}