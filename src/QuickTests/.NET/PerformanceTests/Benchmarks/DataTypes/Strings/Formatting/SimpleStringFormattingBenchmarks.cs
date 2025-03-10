using BenchmarkDotNet.Attributes;
using System.Text;

namespace PerformanceTests.Benchmarks.DataTypes.Strings.Formatting;

[MemoryDiagnoser]
public class SimpleStringFormattingBenchmarks
{
    private readonly Parameters _parameters = new();

    [Benchmark]
    public string StringConcatenation() => _parameters.RendererName + " on " + _parameters.ObjectName + "\n";


    [Benchmark]
    public string StringBuilder() => new StringBuilder()
        .Append(_parameters.RendererName)
        .Append(" on ")
        .Append(_parameters.ObjectName)
        .Append('\n')
        .ToString();

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