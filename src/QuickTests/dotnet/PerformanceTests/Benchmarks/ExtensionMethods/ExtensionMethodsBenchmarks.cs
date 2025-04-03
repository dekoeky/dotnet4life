using BenchmarkDotNet.Attributes;
using PerformanceTests.Benchmarks.ExtensionMethods.Types;

namespace PerformanceTests.Benchmarks.ExtensionMethods;

public class ExtensionMethodsBenchmarks
{
    private const int A = 5;
    private const int B = 2;

    private static readonly IMyInterface AsInterface = new MyImplementation1
    {
        NumberA = A,
        NumberB = B,
    };

    private static readonly MyImplementation1 AsImplementation = new()
    {
        NumberA = A,
        NumberB = B,
    };

    [Benchmark]
    public int Interface_Generic_ExplicitAsInterface() => AsInterface.GetSumGeneric<IMyInterface>();
    [Benchmark]
    public int Interface_Generic_ImplicitAsInterface() => AsInterface.GetSumGeneric();

    [Benchmark]
    public int Interface_AsInterface() => AsInterface.GetSumInterface();


    [Benchmark]
    public int Implementation_Generic_AsInterface() => AsImplementation.GetSumGeneric<IMyInterface>();
    [Benchmark]
    public int Implementation_Generic_ExplicitAsImplementation() => AsImplementation.GetSumGeneric<MyImplementation1>();
    [Benchmark]
    public int Implementation_Generic_ImplicitAsImplementation() => AsImplementation.GetSumGeneric();
    [Benchmark]
    public int Implementation_AsInterface() => AsImplementation.GetSumInterface();
}