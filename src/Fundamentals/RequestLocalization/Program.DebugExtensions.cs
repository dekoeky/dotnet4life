using Microsoft.AspNetCore.Localization;
using RequestLocalization.Middlewares;

namespace RequestLocalization;

public static class DebugExtensions
{
    public static IApplicationBuilder UseRequestLocalization(this IApplicationBuilder app, bool useDebugger)
    {
        ArgumentNullException.ThrowIfNull(app);

        return useDebugger
            ? app.UseMiddleware<DebugRequestLocalizationMiddleware>()
            : app.UseMiddleware<RequestLocalizationMiddleware>();
    }
}