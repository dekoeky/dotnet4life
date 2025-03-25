using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WebApplication1.HealthChecks;
using WebApplication1.HealthChecks.Checks;

namespace WebApplication1.Configuration;

internal static class HealthChecks
{
    /// <summary>
    /// Registers the required HealthChecks for this application.
    /// </summary>
    public static void AddApplicationHealthChecks(this IServiceCollection services)
    {
        //Register the needed dependencies for Health Checks
        var hc = services.AddHealthChecks();

        //Register the Health Checks we see fit for this application

        //A check with no other purpose than to serve as an example
        hc.AddCheck<AlwaysHealthyHealthCheck>("AlwaysHealthy", HealthStatus.Degraded, [Tags.Ready]);

        //A check to see if our application initialization is finished
        hc.AddInitializationCheck();

        //Two checks that ping the google DNS services, for example for internet connectivity
        hc.AddPingCheck("GooglePrimaryDns", "8.8.8.8", HealthStatus.Unhealthy, [Tags.Ready]);
        hc.AddPingCheck("GoogleSecondaryDns", "8.8.4.4", HealthStatus.Unhealthy, [Tags.Ready]);

        //Two checks that check if it's possible to get an HTTP response from Google & Bing
        hc.AddHttpCheck("Google", new Uri("https://www.google.com/"), HealthStatus.Unhealthy, [Tags.Ready]);
        hc.AddHttpCheck("Bing", new Uri("https://www.bing.com/"), HealthStatus.Unhealthy, [Tags.Ready]);
    }

    /// <summary>
    /// Maps the required HealthCheck Endpoints.
    /// </summary>
    public static void MapApplicationHealthChecks(this WebApplication app)
    {
        //HealthCheck Endpoint that indicates whether the application is live (is the application running?)
        app.MapHealthChecks(Endpoints.Ready, new HealthCheckOptions
        {
            Predicate = hc => hc.Tags.Contains(Tags.Live),
            ResponseWriter = ResponseWriters.WriteLivePlaintext,
        });

        //HealthCheck Endpoint that indicates whether the application is ready to process requests
        app.MapHealthChecks(Endpoints.Ready, new HealthCheckOptions
        {
            Predicate = hc => hc.Tags.Contains(Tags.Ready),
            ResponseWriter = ResponseWriters.WriteReadyPlaintext,
        });

        // -- In a typical setup, the part above is usually enough.
        // -- The stuff below is more for illustration purposes.

        //HealthCheck Endpoint that includes ALL Health Checks (with default ResponseWriter, and other options)
        app.MapHealthChecks(Endpoints.All);

        //HealthCheck Endpoint that will 'explain' the HealthCheck result, by printing each entry as json
        app.MapHealthChecks(Endpoints.ExplainJson, new HealthCheckOptions
        {
            //Predicate = //Default (all)
            ResponseWriter = ResponseWriters.WriteJson,
        });
    }
}
