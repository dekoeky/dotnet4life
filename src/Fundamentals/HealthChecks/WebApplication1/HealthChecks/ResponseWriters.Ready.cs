using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WebApplication1.HealthChecks;

public static partial class ResponseWriters
{
    private static readonly byte[] ReadyBytes = "ready"u8.ToArray();
    private static readonly byte[] UnReadyBytes = "unready"u8.ToArray();

    public static Task WriteReadyPlaintext(HttpContext httpContext, HealthReport result)
    {
        httpContext.Response.ContentType = "text/plain";

        var bytes = result.Status == HealthStatus.Healthy ? ReadyBytes : UnReadyBytes;

        return httpContext.Response.Body.WriteAsync(bytes).AsTask();
    }
}