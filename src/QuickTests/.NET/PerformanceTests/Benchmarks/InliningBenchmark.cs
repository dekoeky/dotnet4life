using BenchmarkDotNet.Attributes;
using System.Runtime.CompilerServices;

namespace PerformanceTests.Benchmarks;

public class InliningBenchmark
{
    private int _value = 42;

    // Aggressive inlining: Compiler will try to inline this
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private int InlineMethod(int x) => x * x;

    // No inlining: Compiler is forced to not inline this
    [MethodImpl(MethodImplOptions.NoInlining)]
    private int NoInlineMethod(int x) => x * x;

    private int Unspecified(int x) => x * x;

    [Benchmark]
    public int InlineMethod_() => InlineMethod(_value);

    [Benchmark]
    public int NoInlineMethod() => NoInlineMethod(_value);

    [Benchmark]
    public int Unspecified() => Unspecified(_value);
}