#if DEBUG

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace RequestLocalization.Controllers;

public sealed partial class GreetingController
{
    [Obsolete("Development Only")]
    [HttpGet("Cultures")]
    public object GetCultures()
    {
        var cultures = new Dictionary<string, CultureInfo?>
        {
            { nameof(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture},
            { nameof(CultureInfo.CurrentUICulture), CultureInfo.CurrentUICulture},
            { nameof(CultureInfo.DefaultThreadCurrentCulture), CultureInfo.DefaultThreadCurrentCulture},
            { nameof(CultureInfo.DefaultThreadCurrentUICulture), CultureInfo.DefaultThreadCurrentUICulture},
            { nameof(CultureInfo.InstalledUICulture), CultureInfo.InstalledUICulture},
        };

        var cultureOutput = cultures.ToDictionary(kv => kv.Key, kv => kv.Value is null ? null : new
        {
            kv.Value.Name,
            kv.Value.NativeName,
            kv.Value.EnglishName,
            kv.Value.IetfLanguageTag,
            kv.Value.TwoLetterISOLanguageName,
            kv.Value.ThreeLetterISOLanguageName,
            kv.Value.ThreeLetterWindowsLanguageName,
        });

        return cultureOutput;
    }


    [Obsolete("Development Only")]
    [HttpGet("AllLocalizedStrings")]
    public IEnumerable<LocalizedString> GetAllLocalizedStrings() => localizer.GetAllStrings();
}

#endif