using BenchmarkDotNet.Attributes;

namespace PerformanceTests.Benchmarks.DataTypes.NumericTypes;

public class MultiplyVsDivide
{
    [Params(33.4)]
    public double a;

    [Params(11.1)]
    public double b;

    public double bInvers;

    [GlobalSetup]
    public void Setup() => bInvers = 1 / b;

    [Benchmark] public double Divide() => a / b;
    [Benchmark] public double Multiply() => a * bInvers;
}