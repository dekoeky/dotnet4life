using QuickTests.TestModels;
using System.Text.Json.Serialization;

namespace QuickTests.Json.SourceGeneration;

[JsonSerializable(typeof(WeatherForecast))]
[JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class SourceGenerationContext : JsonSerializerContext;