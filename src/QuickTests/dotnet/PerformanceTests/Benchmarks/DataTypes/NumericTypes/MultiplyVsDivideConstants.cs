using BenchmarkDotNet.Attributes;

namespace PerformanceTests.Benchmarks.DataTypes.NumericTypes;

public class MultiplyVsDivideConstants
{
    private const double RadiansPerDegree = 2 * Math.PI / 360;
    private const double DegreesPerRadian = 1 / RadiansPerDegree;

    [Params(66.4)]
    public double a;

    [Benchmark] public double Divide() => a / DegreesPerRadian;
    [Benchmark] public double Multiply() => a * RadiansPerDegree;
}