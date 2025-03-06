using BenchmarkDotNet.Attributes;

namespace PerformanceTests.Benchmarks.DataTypes.Strings;

[MemoryDiagnoser]
public class StringFormattingBenchmarks
{
    private readonly Parameters _parameters = new();

    [Benchmark]
    // ReSharper disable once UseStringInterpolation
    public string StringFormat() => string.Format("{0} on {1}\n", _parameters.RendererName, _parameters.ObjectName);

    [Benchmark]
    public string StringInterpolation() => $"{_parameters.RendererName} on {_parameters.ObjectName}\n";

    private class Parameters
    {
        public string RendererName { get; } = "RendererName";
        public string ObjectName { get; } = "ObjectName";
    }
}