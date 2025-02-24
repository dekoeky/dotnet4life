using System.Text.Json.Serialization;

namespace QuickTests.Json.Modifiers.IgnoreBaseTypeProperties.Models;

internal class A
{
    [JsonPropertyName("__A__")]
    public string? AProperty { get; set; } = $"{nameof(A)}.{nameof(AProperty)}";
}