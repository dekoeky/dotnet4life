using System.Text.Json.Serialization;

namespace QuickTests.Json.Modifiers.IgnoreBaseTypeProperties.Models;

internal class D : C
{
    [JsonPropertyName("__D__")]
    public string? DProperty { get; set; } = $"{nameof(D)}.{nameof(DProperty)}";
}