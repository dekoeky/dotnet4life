using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WebApplication1.HealthChecks.Checks;

internal static class HttpRequestHealthCheckDependencyInjection
{
    public static void AddHttpCheck(this IHealthChecksBuilder builder,
          string name,
          Uri uri,
          HealthStatus? failureStatus,
          IEnumerable<string> tags)
    {
        builder.Add(new HealthCheckRegistration(name, f => Factory(f, uri), failureStatus, tags));
        builder.Services.AddHttpClient();
    }

    private static HttpRequestHealthCheck Factory(IServiceProvider serviceProvider, Uri uri)
    {
        var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient();

        return new HttpRequestHealthCheck(httpClient, uri);
    }
}
