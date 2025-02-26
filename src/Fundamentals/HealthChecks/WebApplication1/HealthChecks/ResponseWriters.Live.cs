using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WebApplication1.HealthChecks;

/// <summary>
/// Delegates for writing <see cref="HealthReport"/>.
/// </summary>
public static partial class ResponseWriters
{
    private static readonly byte[] LiveBytes = "live"u8.ToArray();
    private static readonly byte[] NotLiveBytes = "not live"u8.ToArray();

    public static Task WriteLivePlaintext(HttpContext httpContext, HealthReport result)
    {
        httpContext.Response.ContentType = "text/plain";

        var bytes = result.Status == HealthStatus.Healthy ? LiveBytes : NotLiveBytes;

        return httpContext.Response.Body.WriteAsync(bytes).AsTask();
    }
}