using BenchmarkDotNet.Attributes;
using SharedLibrary.Interfaces;

namespace PerformanceTests.Benchmarks.Interfaces;

[MemoryDiagnoser]
public class ExplicitVsImplicitInterfaceBenchmarks
{
    private ImplicitIntAdder @implicit;
    private ExplicitIntAdder @explicit;
    private IIntAdder iImplicit;
    private IIntAdder iExplicit;
    private IIntAdder iExplicitOnly;

    [GlobalSetup]
    public void Setup()
    {
        @implicit = new();
        @explicit = new();
        iImplicit = @implicit;
        iExplicit = @explicit;
        iExplicitOnly = new ExplicitOnlyIntAdder();
    }

    [Params(1)]
    public int a;

    [Params(2)]
    public int b;


    [Benchmark] public int Implicit() => @implicit.Add(a, b);
    [Benchmark] public int Explicit() => @explicit.Add(a, b);
    [Benchmark] public int IImplicit() => iImplicit.Add(a, b);
    [Benchmark] public int IExplicit() => iExplicit.Add(a, b);
    [Benchmark] public int IExplicitOnly() => iExplicitOnly.Add(a, b);

    // * Summary *

    //BenchmarkDotNet v0.15.0, Windows 11 (10.0.22631.5771/23H2/2023Update/SunValley3)
    //13th Gen Intel Core i9-13950HX 2.20GHz, 1 CPU, 32 logical and 24 physical cores
    //    .NET SDK 10.0.100-preview.7.25380.108
    //[Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2[AttachedDebugger]
    //DefaultJob : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2


    //| Method        | a | b | Mean      | Error     | StdDev    | Median    | Allocated |
    //|-------------- |-- |-- |----------:|----------:|----------:|----------:|----------:|
    //| Implicit      | 1 | 2 | 0.0060 ns | 0.0081 ns | 0.0165 ns | 0.0000 ns |         - |
    //| Explicit      | 1 | 2 | 0.0371 ns | 0.0230 ns | 0.0291 ns | 0.0304 ns |         - |
    //| IImplicit     | 1 | 2 | 0.0305 ns | 0.0231 ns | 0.0316 ns | 0.0295 ns |         - |
    //| IExplicit     | 1 | 2 | 0.2243 ns | 0.0302 ns | 0.0382 ns | 0.2290 ns |         - |
    //| IExplicitOnly | 1 | 2 | 0.0581 ns | 0.0267 ns | 0.0365 ns | 0.0640 ns |         - |
}