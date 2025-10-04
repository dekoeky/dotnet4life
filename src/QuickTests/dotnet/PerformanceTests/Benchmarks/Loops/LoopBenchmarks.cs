using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System.Diagnostics.CodeAnalysis;

namespace PerformanceTests.Benchmarks.Loops;

[MemoryDiagnoser]
public class LoopBenchmarks
{
    [Params(2, 100, 10000)]
    public int NumberOfDataPoints;

    private readonly Consumer _consumer = new();

    private int[] _arrayOfInt = null!;
    private List<int> _listOfInt = null!;
    private Dictionary<int, int> _dictionaryOfInt = null!;

    [GlobalSetup]
    public void Setup()
    {
        _listOfInt = [.. Enumerable.Range(1, NumberOfDataPoints)];
        _arrayOfInt = [.. _listOfInt];
        _dictionaryOfInt = _listOfInt.ToDictionary(value => value, value => value);
    }

    [Benchmark(Description = "Loop (foreach) every value of a list of int")]
    public void ListOfInt_Foreach()
    {
        foreach (var value in _listOfInt)
            _consumer.Consume(value);
    }

    [Benchmark(Description = "Loop (foreach) every value of an array of int")]
    public void ArrayOfInt_Foreach()
    {
        foreach (var value in _arrayOfInt)
            _consumer.Consume(value);
    }

    [Benchmark(Description = "Loop (foreach) every KeyValuePair of a Dictionary<int, int>, consume the KeyValuePair")]
    public void DictionaryOfInt_ForeachKeyValue_ConsumeKeyValue()
    {
        foreach (var kv in _dictionaryOfInt)
            _consumer.Consume(kv);
    }

    [Benchmark(Description = "Loop (foreach) every KeyValuePair of a Dictionary<int, int>, consume the key")]
    public void DictionaryOfInt_ForeachKeyValue_ConsumeKey()
    {
        foreach (var kv in _dictionaryOfInt)
            _consumer.Consume(kv.Key);
    }

    [Benchmark(Description = "Loop (foreach) every KeyValuePair of a Dictionary<int, int>, consume the value")]
    public void DictionaryOfInt_ForeachKeyValue_ConsumeValue()
    {
        foreach (var kv in _dictionaryOfInt)
            _consumer.Consume(kv.Value);
    }

    [Benchmark(Description = "Loop (foreach) every KeyValuePair of a Dictionary<int, int>, deconstruct the KeyValuePair, consume the value")]
    public void DictionaryOfInt_ForeachDeconstructedValue_ConsumeValue()
    {
        foreach (var (_, value) in _dictionaryOfInt)
            _consumer.Consume(value);
    }

    [Benchmark(Description = "Loop (foreach) every Value of a Dictionary<int, int>.ValueCollection")]
    public void DictionaryOfInt_ForeachValue_ConsumeValue()
    {
        foreach (var value in _dictionaryOfInt.Values)
            _consumer.Consume(value);
    }

    [Benchmark(Description = "Loop (for) every value of a list of int")]
    [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
    public void ListOfInt_For()
    {
        for (var i = 0; i < _listOfInt.Count; i++)
            _consumer.Consume(_listOfInt[i]);
    }

    [Benchmark(Description = "Loop (for) every value of an array of int")]
    [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
    public void ArrayOfInt_For()
    {
        for (var i = 0; i < _arrayOfInt.Length; i++)
            _consumer.Consume(_arrayOfInt[i]);
    }

    [Benchmark(Description = "Loop (for) every Value of a Dictionary<int, int>.ValueCollection")]
    public void DictionaryOfInt_ForValue_ConsumeValue()
    {
        foreach (var key in _dictionaryOfInt.Keys)
            _consumer.Consume(_dictionaryOfInt[key]);
    }
}
