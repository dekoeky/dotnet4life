using WebApplication1.Models;

namespace WebApplication1.MinimalApis;

internal static class ResultsDemoEndpoints
{
    public static IEndpointRouteBuilder MapResultsDemoEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("ResultsDemo");

        group.NewMethod();
        group.MapTypedEndpoints();

        return endpoints;
    }

    private static void MapTypedEndpoints(this RouteGroupBuilder group)
    {
        var typed = group.MapGroup("typed");

        typed.MapGet("ok", () => TypedResults.Ok(new WeatherForecast()));
        typed.MapGet("notfound", TypedResults.NotFound);
    }

    private static void NewMethod(this RouteGroupBuilder group)
    {
        var regular = group.MapGroup("regular");

        regular.MapGet("ok", () => Results.Ok(new WeatherForecast()));
        regular.MapGet("notfound", () => Results.NotFound());
    }
}
