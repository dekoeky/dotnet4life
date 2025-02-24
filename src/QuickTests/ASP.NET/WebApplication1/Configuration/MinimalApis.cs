using WebApplication1.MinimalApis;

namespace WebApplication1.Configuration;

internal static class MinimalApis
{
    public static void MapApplicationMinimalApis(this IEndpointRouteBuilder endpoints)
    {
        //To separate from controller routes
        var gr = endpoints.MapGroup("MinimalApis");

        gr.MapDemoEndpoints();
        gr.MapWeatherForecastEndpoints();
        gr.MapResultsDemoEndpoints();
        gr.MapTimeEndpoints();
    }
}