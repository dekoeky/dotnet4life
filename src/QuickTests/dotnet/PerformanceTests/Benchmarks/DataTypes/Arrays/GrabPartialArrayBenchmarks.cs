using BenchmarkDotNet.Attributes;

namespace PerformanceTests.Benchmarks.DataTypes.Arrays;

[MemoryDiagnoser]
public class GrabPartialArrayBenchmarks
{
    private static readonly int[] InputData = Enumerable.Range(0, 100).ToArray();

    public static readonly int Start = 50;
    public static readonly int Length = 10;

    [Benchmark]
    public int[] ArrayCopy() => MethodsUnderTest.ArrayCopy(InputData, Start, Length);

    [Benchmark]
    public int[] SpanSlice() => MethodsUnderTest.SpanSlice(InputData, Start, Length);

    [Benchmark]
    public int[] MemorySlice() => MethodsUnderTest.MemorySlice(InputData, Start, Length);
}


public static class MethodsUnderTest
{
    public static T[] ArrayCopy<T>(T[] data, int start, int length)
    {
        var result = new T[length];
        Array.Copy(data, start, result, 0, length);
        return result;
    }

    public static T[] SpanSlice<T>(T[] array, int start, int length) => array.AsSpan(start, length).ToArray();
    public static T[] MemorySlice<T>(T[] array, int start, int length) => array.AsMemory(start, length).ToArray();
}