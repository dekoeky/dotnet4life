using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace PerformanceTests.Benchmarks.Loops;

[MemoryDiagnoser]
public class LoopBenchmarks
{
    [Params(2, 100, 10000)]
    public int NumberOfDataPoints;

    private readonly Consumer _consumer = new Consumer();

    private int[] ArrayOfInt;
    private List<int> ListOfInt;
    private Dictionary<int, int> DictionaryOfInt;

    [GlobalSetup]
    public void Setup()
    {
        ListOfInt = Enumerable.Range(1, NumberOfDataPoints).ToList();
        ArrayOfInt = ListOfInt.ToArray();
        DictionaryOfInt = ListOfInt.ToDictionary(value => value, value => value);
    }

    [Benchmark(Description = "Loop (foreach) every value of a list of int")]
    public void ListOfInt_Foreach()
    {
        foreach (var value in ListOfInt)
            _consumer.Consume(value);
    }

    [Benchmark(Description = "Loop (foreach) every value of an array of int")]
    public void ArrayOfInt_Foreach()
    {
        foreach (var value in ArrayOfInt)
            _consumer.Consume(value);
    }

    [Benchmark(Description = "Loop (foreach) every KeyValuePair of a Dictionary<int, int>, consume the KeyValuePair")]
    public void DictionaryOfInt_ForeachKeyValue_ConsumeKeyValue()
    {
        foreach (var kv in DictionaryOfInt)
            _consumer.Consume(kv);
    }

    [Benchmark(Description = "Loop (foreach) every KeyValuePair of a Dictionary<int, int>, consume the key")]
    public void DictionaryOfInt_ForeachKeyValue_ConsumeKey()
    {
        foreach (var kv in DictionaryOfInt)
            _consumer.Consume(kv.Key);
    }

    [Benchmark(Description = "Loop (foreach) every KeyValuePair of a Dictionary<int, int>, consume the value")]
    public void DictionaryOfInt_ForeachKeyValue_ConsumeValue()
    {
        foreach (var kv in DictionaryOfInt)
            _consumer.Consume(kv.Value);
    }

    [Benchmark(Description = "Loop (foreach) every KeyValuePair of a Dictionary<int, int>, deconstruct the KeyValuePair, consume the value")]
    public void DictionaryOfInt_ForeachDeconstructedValue_ConsumeValue()
    {
        foreach (var (_, value) in DictionaryOfInt)
            _consumer.Consume(value);
    }

    [Benchmark(Description = "Loop (foreach) every Value of a Dictionary<int, int>.ValueCollection")]
    public void DictionaryOfInt_ForeachValue_ConsumeValue()
    {
        foreach (var value in DictionaryOfInt.Values)
            _consumer.Consume(value);
    }

    [Benchmark(Description = "Loop (for) every value of a list of int")]
    public void ListOfInt_For()
    {
        for (var i = 0; i < ListOfInt.Count; i++)
            _consumer.Consume(ListOfInt[i]);
    }

    [Benchmark(Description = "Loop (for) every value of an array of int")]
    public void ArrayOfInt_For()
    {
        for (var i = 0; i < ArrayOfInt.Length; i++)
            _consumer.Consume(ArrayOfInt[i]);
    }

    [Benchmark(Description = "Loop (for) every Value of a Dictionary<int, int>.ValueCollection")]
    public void DictionaryOfInt_ForValue_ConsumeValue()
    {
        foreach (var key in DictionaryOfInt.Keys)
            _consumer.Consume(DictionaryOfInt[key]);
    }
}
