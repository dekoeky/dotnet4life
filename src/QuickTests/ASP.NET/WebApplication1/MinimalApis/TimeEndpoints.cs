namespace WebApplication1.MinimalApis;

internal static class TimeEndpoints
{
    public static void MapTimeEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/TimeOfDay", () => DateTime.Now.TimeOfDay)
            .WithName("GetTimeOfDay")
            .WithOpenApi();

        endpoints.MapGet("/Timestamp", () => DateTime.Now)
            .WithName("GetCurrentTimestamp")
            .WithOpenApi();

        endpoints.MapGet("/Date", () => DateOnly.FromDateTime(DateTime.Today))
            .WithName("GetCurrentDate")
            .WithOpenApi();
    }
}