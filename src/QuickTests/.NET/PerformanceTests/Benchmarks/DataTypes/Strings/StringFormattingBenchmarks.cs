using BenchmarkDotNet.Attributes;
using System.Diagnostics.CodeAnalysis;

namespace PerformanceTests.Benchmarks.DataTypes.Strings;

[MemoryDiagnoser]
public class StringFormattingBenchmarks
{
    private readonly Parameters _parameters = new();

    [Benchmark]
    [SuppressMessage("ReSharper", "UseStringInterpolation")]
    public string StringFormat() => string.Format("{0} on {1}\n", _parameters.RendererName, _parameters.ObjectName);

    [Benchmark]
    public string StringInterpolation() => $"{_parameters.RendererName} on {_parameters.ObjectName}\n";

    class Parameters
    {
        public string RendererName { get; } = "RendererName";
        public string ObjectName { get; } = "ObjectName";
    }
}