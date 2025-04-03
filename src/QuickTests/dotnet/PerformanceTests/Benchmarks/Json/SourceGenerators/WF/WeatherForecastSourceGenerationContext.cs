using System.Text.Json.Serialization;

namespace PerformanceTests.Benchmarks.Json.SourceGenerators.WF;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(WeatherForecast))]
internal partial class WeatherForecastSourceGenerationContext : JsonSerializerContext;