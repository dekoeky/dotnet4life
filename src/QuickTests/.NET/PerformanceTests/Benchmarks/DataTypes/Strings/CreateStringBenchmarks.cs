using BenchmarkDotNet.Attributes;

namespace PerformanceTests.Benchmarks.DataTypes.Strings;

[MemoryDiagnoser]
public class CreateStringBenchmarks
{
    private string s;
    [Benchmark]
    public string Regular()
    {
        s = "Hello";
        return s;
    }

    [Benchmark]
    public string CharArrayToString()
    {
        s = new string(['H', 'e', 'l', 'l', 'o']);
        return s;
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
        s = new string(_preCreatedCharArray);
        return s;
    }
}