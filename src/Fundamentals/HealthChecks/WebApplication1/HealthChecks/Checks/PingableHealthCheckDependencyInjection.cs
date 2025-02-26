using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WebApplication1.HealthChecks.Checks;

internal static class PingableHealthCheckDependencyInjection
{
    public static void AddPingCheck(this IHealthChecksBuilder builder,
          string name,
          string hostnameOrAddress,
          HealthStatus? failureStatus,
          IEnumerable<string> tags)
         => builder.Add(new HealthCheckRegistration(name, f => new PingableHealthCheck(hostnameOrAddress), failureStatus, tags));
}