using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Globalization;

namespace PerformanceTests.Benchmarks.DataTypes.NumericTypes;

[SimpleJob(RuntimeMoniker.Net47)]
[SimpleJob(RuntimeMoniker.Net471)]
[SimpleJob(RuntimeMoniker.Net472)]
[SimpleJob(RuntimeMoniker.Net48)]
[SimpleJob(RuntimeMoniker.Net481)]
[SimpleJob(RuntimeMoniker.Net70)]
[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
[SimpleJob(RuntimeMoniker.Net10_0)]
[MemoryDiagnoser]
public class FloatToStringFrameworks
{
    [Params(1.2345679f)]
    public float Value;

    [Params( "G")]
    public string? Format;

    private readonly IFormatProvider _culture = CultureInfo.InvariantCulture;

    [Benchmark]
    public string FloatToStringWithFormat() => Value.ToString(Format, _culture);
}