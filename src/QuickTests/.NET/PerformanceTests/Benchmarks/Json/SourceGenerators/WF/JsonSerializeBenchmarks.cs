using BenchmarkDotNet.Attributes;
using System.Text.Json;

namespace PerformanceTests.Benchmarks.Json.SourceGenerators.WF;

[MemoryDiagnoser]
public class JsonSerializeBenchmarks
{
    private readonly WeatherForecast _weatherForecast = new()
    {
        Date = DateOnly.FromDateTime(DateTime.Now),
        Summary = "Today was a good day",
        TemperatureC = 35,
    };


    private readonly JsonSerializerOptions _sourceGenOptions = new()
    {
        TypeInfoResolver = WeatherForecastSourceGenerationContext.Default
    };

    [Benchmark]
    public string SerializeSourceGeneration1()
        => JsonSerializer.Serialize(_weatherForecast, WeatherForecastSourceGenerationContext.Default.WeatherForecast);

    [Benchmark]
    public string SerializeSourceGeneration2()
        => JsonSerializer.Serialize(_weatherForecast, typeof(WeatherForecast), WeatherForecastSourceGenerationContext.Default);

    [Benchmark]
    public string SerializeSourceGeneration3()
        => JsonSerializer.Serialize(_weatherForecast, typeof(WeatherForecast), _sourceGenOptions);

    private readonly JsonSerializerOptions _reflectionJsonSerializerOptions = new()
    {
        WriteIndented = true,
    };

    [Benchmark]
    public string SerializeReflection() => JsonSerializer.Serialize(_weatherForecast, _reflectionJsonSerializerOptions);

}