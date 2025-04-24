using BenchmarkDotNet.Attributes;

namespace PerformanceTests.Benchmarks.DataTypes.Strings;

[MemoryDiagnoser]
public class StringCreateBenchmarks
{
    private string _s = string.Empty;

    [Benchmark]
    public string Constant()
    {
        _s = "Hello";
        return _s;
    }

    [Benchmark]
    public string CharArrayToString()
    {
        _s = new string(['H', 'e', 'l', 'l', 'o']);
        return _s;
    }

    [Benchmark]
    public string SpanToArray()
    {
        Span<char> span = ['H', 'e', 'l', 'l', 'o'];
        _s = new string(span);
        return _s;
    }

    [Benchmark]
    public string StringCreate()
    {
        _s = string.Create(5, 0, (span, _) =>
        {
            span[0] = 'H';
            span[1] = 'e';
            span[2] = 'l';
            span[3] = 'l';
            span[4] = 'o';
        });
        return _s;
    }
}