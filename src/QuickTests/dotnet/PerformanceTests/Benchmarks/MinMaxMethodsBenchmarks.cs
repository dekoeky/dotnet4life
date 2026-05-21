using BenchmarkDotNet.Attributes;
using SharedLibrary.DataTypes.NumericTypes;

namespace PerformanceTests.Benchmarks;

/// <summary>
/// <see cref="MinMaxMethods"/> related Benchmarks.
/// </summary>
public class MinMaxMethodsBenchmarks
{
    private static readonly Random random = new(1);

    [Params(1, 10, 100, 1000, 10000)]
    public int n;

    private MinMaxMethods.DemoPoint[] points = [];

    [GlobalSetup]
    public void GlobalSetup()
    {
        points = new MinMaxMethods.DemoPoint[n];

        for (var i = 0; i < n; i++)
            points[i] = new MinMaxMethods.DemoPoint(random.Next(), random.Next(), random.Next());
    }

    [Benchmark]
    public (int minX, int maxX, int minY, int maxY) Loop_MinMaxXY()
        => MinMaxMethods.MinMaxXY_ViaLoop(points);

    [Benchmark]
    public (int minX, int maxX, int minY, int maxY) Linq_MinMaxXY()
        => MinMaxMethods.MinMaxXY_ViaLinq(points);

    [Benchmark]
    public (int Min, int Max)[] Generic_MinMaxXY()
        => MinMaxMethods.GenericMinMax(points,
            pt => pt.X,
            pt => pt.Y);


    [Benchmark]
    public (int minX, int maxX, int minY, int maxY, int minZ, int maxZ) Loop_MinMaxXYZ()
        => MinMaxMethods.MinMaxXYZ_ViaLoop(points);

    [Benchmark]
    public (int minX, int maxX, int minY, int maxY, int minZ, int maxZ) Linq_MinMaxXYZ()
        => MinMaxMethods.MinMaxXYZ_ViaLinq(points);

    [Benchmark]
    public (int Min, int Max)[] Generic_MinMaxXYZ()
        => MinMaxMethods.GenericMinMax(points,
            pt => pt.X,
            pt => pt.Y,
            pt => pt.Z);
}
