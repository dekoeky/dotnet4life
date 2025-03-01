using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.NetworkInformation;

namespace WebApplication1.HealthChecks.Checks;

internal class PingableHealthCheck(string hostnameOrAddress) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            using var ping = new Ping();
            var response = await ping.SendPingAsync(hostnameOrAddress);

            if (response.Status == IPStatus.Success) return HealthCheckResult.Healthy();

            return HealthCheckResult.Unhealthy(response.Status.ToString());
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("Error occurred", ex);
        }
    }
}
