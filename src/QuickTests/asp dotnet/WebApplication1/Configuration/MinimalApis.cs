using WebApplication1.MinimalApis;
using WebApplication1.MinimalApis.Export;

namespace WebApplication1.Configuration;

internal static class MinimalApis
{
    /// <summary>
    /// Maps all this application's minimal api endpoints.
    /// </summary>
    public static void MapApplicationMinimalApis(this IEndpointRouteBuilder endpoints)
    {
        //To separate from controller routes
        var gr = endpoints.MapGroup("MinimalApis");

        gr.MapDemoEndpoints();
        gr.MapWeatherForecastEndpoints();
        gr.MapResultsDemoEndpoints();
        gr.MapTimeEndpoints();
        gr.MapDownloadDemonstrationEndpoints();
    }
}