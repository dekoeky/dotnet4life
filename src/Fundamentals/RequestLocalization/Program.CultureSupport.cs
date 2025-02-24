namespace RequestLocalization;

internal static class CultureSupport
{
    //TODO: Unit Test if all cultures .resx files are present

    public const string DefaultCulture = "en-US";
    public static readonly string[] SupportedCultures = [
        DefaultCulture,
        "fr",
        "nl",
        "nl-NL",
        "nl-BE"
    ];
}