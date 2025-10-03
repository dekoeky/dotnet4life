using BenchmarkDotNet.Attributes;
using System.Globalization;

namespace PerformanceTests.Benchmarks.DataTypes.NumericTypes;

[MemoryDiagnoser]
public class FloatToStringFormats
{
    [Params(0.1f, 1.2345679f)]
    public float Value;

    [Params(null, "G", "G7", "G9", "R")]
    public string? Format;

    private readonly IFormatProvider _culture = CultureInfo.InvariantCulture;

    [Benchmark]
    public string FloatToStringWithFormat() => Value.ToString(Format, _culture);
}