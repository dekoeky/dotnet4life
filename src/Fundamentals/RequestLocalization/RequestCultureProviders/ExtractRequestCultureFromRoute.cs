using Microsoft.AspNetCore.Localization;

namespace RequestLocalization.RequestCultureProviders;

[Obsolete($"Use {nameof(Microsoft.AspNetCore.Localization.Routing.RouteDataRequestCultureProvider)} instead", true)]
internal sealed class ExtractRequestCultureFromRoute : RequestCultureProvider
{
    public string RouteStringKey { get; set; } = "culture";

    public string UIRouteStringKey { get; set; } = "ui-culture";

    /// <inheritdoc />
    public override Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
    {
        ArgumentNullException.ThrowIfNull(httpContext);
        var request = httpContext.Request;

        string? routeCulture = null;
        string? routeUiCulture = null;

        if (!string.IsNullOrWhiteSpace(RouteStringKey))
        {
            routeCulture = request.RouteValues[RouteStringKey]?.ToString();
        }

        if (!string.IsNullOrWhiteSpace(UIRouteStringKey))
        {
            routeUiCulture = request.RouteValues[UIRouteStringKey]?.ToString();
        }

        if (routeCulture == null && routeUiCulture == null)
        {
            // No values specified for either so no match
            return NullProviderCultureResult;
        }

        if (routeCulture != null && routeUiCulture == null)
        {
            // Value for culture but not for UI culture so default to culture value for both
            routeUiCulture = routeCulture;
        }
        else if (routeCulture == null && routeUiCulture != null)
        {
            // Value for UI culture but not for culture so default to UI culture value for both
            routeCulture = routeUiCulture;
        }

        var providerResultCulture = new ProviderCultureResult(routeCulture, routeUiCulture);

        return Task.FromResult<ProviderCultureResult?>(providerResultCulture);
    }

    //private static bool IsIetfLanguageTag(string tag)
    //{
    //    try
    //    {
    //        // Try to create a CultureInfo object
    //        var culture = new CultureInfo(tag);

    //        // Check if the tag matches the original input
    //        return culture.IetfLanguageTag.Equals(tag, StringComparison.OrdinalIgnoreCase);
    //    }
    //    catch (CultureNotFoundException)
    //    {
    //        // If a CultureNotFoundException is thrown, the tag is not valid
    //        return false;
    //    }
    //}
}