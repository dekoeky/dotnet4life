using System.Text.Json.Serialization;

namespace QuickTests.Json.Modifiers.IgnoreBaseTypeProperties.Models;

internal class E : D
{
    [JsonPropertyName("__E__")]
    public string? EProperty { get; set; } = $"{nameof(E)}.{nameof(EProperty)}";
}