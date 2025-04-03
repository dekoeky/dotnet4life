using System.Text.Json.Serialization;

namespace QuickTests.Json.Modifiers.IgnoreBaseTypeProperties.Models;

internal class B : A
{
    [JsonPropertyName("__B__")]
    public string? BProperty { get; set; } = $"{nameof(B)}.{nameof(BProperty)}";
}