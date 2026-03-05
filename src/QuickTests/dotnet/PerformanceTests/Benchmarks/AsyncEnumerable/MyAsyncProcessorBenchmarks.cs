using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using SharedLibrary.AsyncEnumerable;

namespace PerformanceTests.Benchmarks.AsyncEnumerable;

[ShortRunJob]
[MemoryDiagnoser]
public class MyAsyncProcessorBenchmarks
{
    private MyAsyncProcessor _processor;
    private readonly Consumer _consumer = new();

    [ParamsSource(nameof(TestData))]
    public (int processingTime, int queryTime, int n) Parameters;


    public static IEnumerable<(int processingTime, int queryTime, int n)> TestData()
    {
        yield return (processingTime: 10, queryTime: 10, n: 5);
        yield return (processingTime: 100, queryTime: 100, n: 5);
        yield return (processingTime: 1000, queryTime: 1000, n: 5);
    }

    [GlobalSetup]
    public void GlobalSetup()
    {
        _processor = new MyAsyncProcessor(Parameters.processingTime, Parameters.queryTime);
    }


    [Benchmark(Baseline = true)]
    public async Task RetrieveAndProcessData()
    {
        await foreach (var item in _processor.RetrieveAndProcessData(Parameters.n))
            _consumer.Consume(item);
    }

    [Benchmark]
    public async Task RetrieveAndProcessDataWithChannel()
    {
        await foreach (var item in _processor.RetrieveAndProcessDataWithChannel(Parameters.n))
            _consumer.Consume(item);
    }
}