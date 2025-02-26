using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;

namespace WebApplication1.HealthChecks.Checks;

/// <summary>
/// An example Health Check that attempts to get a success status code from an HTTP Request to a specified <paramref name="uri"/>.
/// </summary>
internal class HttpRequestHealthCheck(HttpClient client, Uri uri) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var start = Stopwatch.GetTimestamp();
            var response = await client.GetAsync(uri, cancellationToken);
            var elapsed = Stopwatch.GetElapsedTime(start);

            var data = new Dictionary<string, object>()
            {
                { "ElapsedTime", elapsed },
                { "StatusCode", response.StatusCode }
            };

            if (response.IsSuccessStatusCode) return HealthCheckResult.Healthy(data: data);

            return HealthCheckResult.Unhealthy(response.StatusCode.ToString(), data: data);
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("Error occurred", ex);
        }
    }
}
