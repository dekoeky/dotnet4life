using System.Diagnostics.CodeAnalysis;

namespace QuickTests;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "public API")]
internal static class StringExtensions
{
    public const string NullString = "<null>";
    public const string EmptyString = "<empty>";

    public static string ReplaceNullsAndEmpties(this string? value) =>
        value switch
        {
            null => NullString,
            "" => EmptyString,
            _ => value
        };
}