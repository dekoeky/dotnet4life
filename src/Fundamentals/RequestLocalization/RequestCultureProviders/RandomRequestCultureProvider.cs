using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

namespace RequestLocalization.RequestCultureProviders;

/// <summary>
/// Provides a Random Request Culture, which results in Random Request Cultures being used.
/// </summary>
internal sealed class RandomRequestCultureProvider : RequestCultureProvider
{
    /// <inheritdoc />
    public override Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
    {
        ArgumentNullException.ThrowIfNull(httpContext);
        Options ??= httpContext.RequestServices.GetService<IOptions<RequestLocalizationOptions>>()?.Value;

        var randomCulture = Random.Shared.NextOrDefault(Options?.SupportedCultures)?.IetfLanguageTag;
        var randomUiCulture = Random.Shared.NextOrDefault(Options?.SupportedUICultures)?.IetfLanguageTag;


        if (randomCulture == null && randomUiCulture != null)
            randomCulture = randomUiCulture;
        else if (randomUiCulture == null && randomCulture != null)
            randomUiCulture = randomCulture;

        var providerResultCulture = new ProviderCultureResult(randomCulture, randomUiCulture);

        return Task.FromResult<ProviderCultureResult?>(providerResultCulture);
    }
}