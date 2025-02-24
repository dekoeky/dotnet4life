using BenchmarkDotNet.Attributes;

namespace PerformanceTests.Benchmarks.ExtensionMethods;

public class ExtensionMethodsBenchmarks
{
    //| Method                                  | Mean      | Error     | StdDev    | Median    |
    //|---------------------------------------- |----------:|----------:|----------:|----------:|
    //| Interface_Generic_AsInterface           | 0.3995 ns | 0.1017 ns | 0.2997 ns | 0.5712 ns |
    //| Interface_AsInterface                   | 0.1582 ns | 0.0293 ns | 0.0558 ns | 0.1657 ns |
    //| Implementation_Generic_AsInterface      | 0.0499 ns | 0.0220 ns | 0.0445 ns | 0.0483 ns |
    //| Implementation_Generic_AsImplementation | 0.0177 ns | 0.0190 ns | 0.0168 ns | 0.0183 ns |
    //| Implementation_AsInterface              | 0.1517 ns | 0.0287 ns | 0.0618 ns | 0.1461 ns |

    private const int A = 5, B = 2;

    private static readonly IMyInterface AsInterface = new MyImplementation1
    {
        Number1 = A,
        Number2 = B,
    };

    private static readonly MyImplementation1 AsImplementation = new()
    {
        Number1 = A,
        Number2 = B,
    };

    [Benchmark]
    public int Interface_Generic_AsInterface() => AsInterface.GetSumGeneric<IMyInterface>();

    [Benchmark]
    public int Interface_AsInterface() => AsInterface.GetSumInterface();

    [Benchmark]
    public int Implementation_Generic_AsInterface() => AsImplementation.GetSumGeneric<IMyInterface>();

    [Benchmark]
    public int Implementation_Generic_AsImplementation() => AsImplementation.GetSumGeneric<MyImplementation1>();

    [Benchmark]
    public int Implementation_AsInterface() => AsImplementation.GetSumInterface();
}