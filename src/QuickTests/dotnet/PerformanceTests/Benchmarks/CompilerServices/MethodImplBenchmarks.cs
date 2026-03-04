using BenchmarkDotNet.Attributes;
using SharedLibrary.CompilerServices;

namespace PerformanceTests.Benchmarks.CompilerServices;

[ShortRunJob]
[MemoryDiagnoser]
public class MethodImplBenchmarks
{
    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(Arguments))]
    public double Inline(double r) => 4.0 / 3.0 * Math.PI * r * r * r;

    [Benchmark]
    [ArgumentsSource(nameof(Arguments))]
    public double Regular(double r) => CompilerServicesDemo.Regular(r);

    [Benchmark]
    [ArgumentsSource(nameof(Arguments))]
    public double AggressiveInlining(double r) => CompilerServicesDemo.AggressiveInlining(r);

    [Benchmark]
    [ArgumentsSource(nameof(Arguments))]
    public double AggressiveOptimization(double r) => CompilerServicesDemo.AggressiveOptimization(r);

    [Benchmark]
    [ArgumentsSource(nameof(Arguments))]
    public double NoInlining(double r) => CompilerServicesDemo.NoInlining(r);

    [Benchmark]
    [ArgumentsSource(nameof(Arguments))]
    public double NoOptimization(double r) => CompilerServicesDemo.NoOptimization(r);

    public static IEnumerable<double> Arguments()
    {
        yield return 2.0;
    }
}