using BenchmarkDotNet.Attributes;
using SharedLibrary.DataTypes.Formatting;

namespace PerformanceTests.Benchmarks.DataTypes.SpanFormattable;

[MemoryDiagnoser]
public class SpanFormattableTests
{
    private readonly ExampleFormattable _example = new();

    [Benchmark]
    public string ToStringTest() => _example.ToString();

    [Benchmark]
    public string ToStringWithFormatTest() => _example.ToString(string.Empty, null);

    [Benchmark]
    public string TryFormatTest()
    {
        //Allocate a span to write to
        Span<char> span = stackalloc char[255];

        //Write to the span
        _example.TryFormat(span, out var charsWritten, string.Empty, null);

        //Convert to string
        return new string(span[..charsWritten]);
    }
}