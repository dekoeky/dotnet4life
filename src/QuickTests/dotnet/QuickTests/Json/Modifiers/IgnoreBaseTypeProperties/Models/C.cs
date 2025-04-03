using System.Text.Json.Serialization;

namespace QuickTests.Json.Modifiers.IgnoreBaseTypeProperties.Models;

internal class C : B
{
    [JsonPropertyName("__C__")]
    public string? CProperty { get; set; } = $"{nameof(C)}.{nameof(CProperty)}";
}