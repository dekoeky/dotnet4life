using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.NetworkInformation;

namespace WebApplication1.HealthChecks.Checks;

/// <summary>
/// A health check that attempts to ping a given <paramref name="hostnameOrAddress"/>.
/// </summary>
/// <remarks>Requires PING to be installed (see dockerfile)</remarks>
/// <param name="hostnameOrAddress"></param>
internal class PingableHealthCheck(string hostnameOrAddress) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            using var ping = new Ping();
            var response = await ping.SendPingAsync(hostnameOrAddress);

            return response.Status == IPStatus.Success
                ? HealthCheckResult.Healthy()
                : HealthCheckResult.Unhealthy(response.Status.ToString());
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("Error occurred", ex);
        }
    }
}
