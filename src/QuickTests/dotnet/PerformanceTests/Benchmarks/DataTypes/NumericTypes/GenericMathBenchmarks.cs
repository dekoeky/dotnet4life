using BenchmarkDotNet.Attributes;
using SharedLibrary.DataTypes.NumericTypes;
using System.ComponentModel;

namespace PerformanceTests.Benchmarks.DataTypes.NumericTypes;

[ShortRunJob]
[MemoryDiagnoser]
public class GenericMathBenchmarks
{
    [Benchmark]
    [Category("double")]
    [ArgumentsSource(nameof(DoubleTestData))]
    public double CircleAreaDouble(double r)
        => GenericMathTestMethods.CircleAreaDouble(r);

    [Benchmark]
    [Category("float")]
    [ArgumentsSource(nameof(FloatTestData))]
    public float CircleAreaFloat(float r)
        => GenericMathTestMethods.CircleAreaFloat(r);

    [Benchmark]
    [Category("double")]
    [ArgumentsSource(nameof(DoubleTestData))]
    public double CircleAreaGenericDouble(double r)
        => GenericMathTestMethods.CircleAreaGeneric(r);

    [Benchmark]
    [Category("float")]
    [ArgumentsSource(nameof(FloatTestData))]
    public float CircleAreaGenericFloat(float r)
        => GenericMathTestMethods.CircleAreaGeneric(r);

    [Benchmark]
    [Category("double")]
    [ArgumentsSource(nameof(DoubleTestData))]
    public double CircleAreaGenericOptimizedDouble(double r)
        => GenericMathTestMethods.CircleAreaGenericOptimized(r);

    [Benchmark]
    [Category("float")]
    [ArgumentsSource(nameof(FloatTestData))]
    public float CircleAreaGenericOptimizedFloat(float r)
        => GenericMathTestMethods.CircleAreaGenericOptimized(r);

    [Benchmark]
    [Category("double")]
    [ArgumentsSource(nameof(DoubleTestData))]
    public double CircleAreaGenericOptimizedDouble2(double r)
        => GenericMathTestMethodsOptimized<double>.CircleAreaGeneric(r);

    [Benchmark]
    [Category("float")]
    [ArgumentsSource(nameof(FloatTestData))]
    public float CircleAreaGenericOptimizedFloat2(float r)
        => GenericMathTestMethodsOptimized<float>.CircleAreaGeneric(r);

    public static IEnumerable<double> DoubleTestData()
    {
        yield return 3d;
    }

    public static IEnumerable<float> FloatTestData()
    {
        yield return 3f;
    }
}