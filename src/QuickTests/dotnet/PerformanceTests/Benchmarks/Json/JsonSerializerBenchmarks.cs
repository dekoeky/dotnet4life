using BenchmarkDotNet.Attributes;
using System.Text.Json;

namespace PerformanceTests.Benchmarks.Json;

//[SimpleJob(warmupCount: 1, iterationCount:1)]
[SimpleJob]
[MemoryDiagnoser]
public class JsonSerializerBenchmarks
{
    private readonly MyPoco _data = new();

    private readonly JsonSerializerOptions _options = new(JsonSerializerOptions.Default)
    {
        IncludeFields = true,
        WriteIndented = true,
    };

    [Benchmark]
    public string SerializeToString() => JsonSerializer.Serialize(_data, _options);

    [Benchmark]
    public JsonDocument SerializeToJsonDocument() => JsonSerializer.SerializeToDocument(_data, _options);

    public class MyPoco
    {
        public float SomeFloat { get; set; } = 3.14f;
        public bool SomeBool { get; set; } = true;
    }
}
