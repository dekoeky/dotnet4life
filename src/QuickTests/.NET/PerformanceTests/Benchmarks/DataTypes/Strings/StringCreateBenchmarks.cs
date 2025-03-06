using BenchmarkDotNet.Attributes;

namespace PerformanceTests.Benchmarks.DataTypes.Strings;

[MemoryDiagnoser]
public class StringCreateBenchmarks
{
    private string _s = string.Empty;

    [Benchmark]
    public string Regular()
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

    private readonly char[] _preCreatedCharArray = new char[5];

    [Benchmark]
    public string PreCreatedCharArrayToString()
    {
        _preCreatedCharArray[0] = 'H';
        _preCreatedCharArray[1] = 'e';
        _preCreatedCharArray[2] = 'l';
        _preCreatedCharArray[3] = 'l';
        _preCreatedCharArray[4] = 'o';
        _s = new string(_preCreatedCharArray);
        return _s;
    }
}