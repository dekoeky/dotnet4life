using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WebApplication1.HealthChecks.Checks;

internal static class PingableHealthCheckDependencyInjection
{
    /// <summary>
    /// Registers a <see cref="PingableHealthCheck"/>.
    /// </summary>
    /// <remarks>Requires PING to be installed (see dockerfile)</remarks>
    public static void AddPingCheck(this IHealthChecksBuilder builder,
          string name,
          string hostnameOrAddress,
          HealthStatus? failureStatus,
          IEnumerable<string> tags)
         => builder.Add(new HealthCheckRegistration(name, f => new PingableHealthCheck(hostnameOrAddress), failureStatus, tags));
}