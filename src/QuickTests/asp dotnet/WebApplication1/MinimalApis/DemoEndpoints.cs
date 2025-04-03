namespace WebApplication1.MinimalApis;

internal static class DemoEndpoints
{
    public static void MapDemoEndpoints(this IEndpointRouteBuilder endpoints)
    {
        //To illustrate
        endpoints.MapGet("WhatDayIsIt", () => DateTime.Today.DayOfWeek);
        endpoints.MapGet("WhatDayIsIt/Json", () => new { DateTime.Today.DayOfWeek });
    }
}