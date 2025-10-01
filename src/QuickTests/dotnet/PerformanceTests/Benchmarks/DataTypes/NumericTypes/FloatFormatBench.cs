using BenchmarkDotNet.Attributes;
using System.Globalization;

namespace PerformanceTests.Benchmarks.DataTypes.NumericTypes;

[MemoryDiagnoser]
public class FloatFormatBench
{
    private readonly float _value = 123456.984f;
    private readonly string _dynamic = "G9"; // simulate caller-provided format
    private readonly IFormatProvider _ci = CultureInfo.InvariantCulture;

    [Benchmark(Baseline = true)]
    public string Fixed_G9()
        => _value.ToString("G9", _ci);

    [Benchmark]
    public string Dynamic_Format_Variable()
        => _value.ToString(_dynamic, _ci);
}