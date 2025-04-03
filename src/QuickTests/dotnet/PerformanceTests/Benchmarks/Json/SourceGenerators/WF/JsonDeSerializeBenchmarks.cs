using BenchmarkDotNet.Attributes;
using System.Text.Json;

namespace PerformanceTests.Benchmarks.Json.SourceGenerators.WF;

[MemoryDiagnoser]
public class JsonDeSerializeBenchmarks
{
    private readonly JsonSerializerOptions _reflectionJsonSerializerOptions = new()
    {
        WriteIndented = true,
    };

    private readonly JsonSerializerOptions _sourceGenOptions = new()
    {
        TypeInfoResolver = WeatherForecastSourceGenerationContext.Default
    };

    private readonly string _json = """
                                    {
                                      "TemperatureC": 35,
                                      "Summary": "Today was a good day",
                                      "TemperatureF": 94,
                                      "Date": "2025-01-13"
                                    }
                                    """;

    [Benchmark]
    public WeatherForecast? DeSerializeSourceGeneration1()
        => JsonSerializer.Deserialize(_json, WeatherForecastSourceGenerationContext.Default.WeatherForecast);
    [Benchmark]
    public WeatherForecast? DeSerializeSourceGeneration2()
        => JsonSerializer.Deserialize(_json, typeof(WeatherForecast), WeatherForecastSourceGenerationContext.Default) as WeatherForecast;
    [Benchmark]
    public WeatherForecast? DeSerializeSourceGeneration3()
        => JsonSerializer.Deserialize(_json, typeof(WeatherForecast), _sourceGenOptions) as WeatherForecast;

    [Benchmark]
    public WeatherForecast? DeSerializeReflection()
        => JsonSerializer.Deserialize<WeatherForecast>(_json, _reflectionJsonSerializerOptions);
}