using BenchmarkDotNet.Attributes;
using System.Globalization;
using System.Text;

namespace PerformanceTests.Benchmarks.DataTypes.Strings.StringBuilders;

[MemoryDiagnoser]
public class StringBuilderTests
{
    private readonly CultureInfo _culture = CultureInfo.InvariantCulture;
    private const string DefaultText = "DefaultText";

    [Params(Math.PI)]
    public double Value;

    [Benchmark] public string Append() => new StringBuilder(DefaultText).Append(Value).ToString();
    [Benchmark] public string AppendToString() => new StringBuilder(DefaultText).Append(Value.ToString(_culture)).ToString();
    [Benchmark] public string AppendFormat() => new StringBuilder(DefaultText).AppendFormat(_culture, "{0}", Value).ToString();
    [Benchmark] public string AppendFormatInterpolated() => new StringBuilder(DefaultText).AppendFormat(_culture, $"{Value}").ToString();
}