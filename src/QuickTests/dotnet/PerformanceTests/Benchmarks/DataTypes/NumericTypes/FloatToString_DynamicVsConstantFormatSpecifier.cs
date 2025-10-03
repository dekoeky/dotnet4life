using BenchmarkDotNet.Attributes;
using System.Globalization;

namespace PerformanceTests.Benchmarks.DataTypes.NumericTypes;

/// <summary>
/// This benchmark shows the difference in performance between using a constant format specifier (compile time) and a format specifier from arguments (JIT).
/// </summary>
[MemoryDiagnoser]
public class FloatToString_DynamicVsConstantFormatSpecifier
{
    private readonly float _value = 123456.984f;
    private readonly IFormatProvider _ci = CultureInfo.InvariantCulture;

    private const string FormatSpecifier = "G9";

    [Benchmark(Baseline = true)]
    public string ConstantFormatSpecifier()
        => _value.ToString(FormatSpecifier, _ci);

    [Benchmark]
    [Arguments(FormatSpecifier)]
    public string DynamicFormatSpecifier(string formatSpecifier)
        => _value.ToString(formatSpecifier, _ci);
}