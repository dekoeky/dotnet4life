using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace PerformanceTests.Benchmarks.DataTypes.Strings;


//[LongRunJob(RuntimeMoniker.Net10_0)]
[ShortRunJob(RuntimeMoniker.Net10_0)]
[MemoryDiagnoser]
public class StringSplitExampleBenchmark
{
    private const char Separator = ',';

    [Benchmark]
    [Arguments("123,987")]
    public (int a, int b) A(string input)
    {
        var parts = input.Split(Separator);

        var a = int.Parse(parts[0]);
        var b = int.Parse(parts[1]);

        return (a, b);
    }

    [Benchmark]
    [Arguments("123,987")]
    public (int a, int b) B(ReadOnlySpan<char> input)
    {
        var s = input.IndexOf(Separator);

        var a = int.Parse(input[..s++]);
        var b = int.Parse(input[s..]);

        return (a, b);
    }

    [Benchmark]
    [Arguments("123,987")]
    public (int a, int b) C(ReadOnlySpan<char> input)
    {
        Span<Range> ranges = stackalloc Range[2];
        if (2 != input.Split(ranges, Separator)) throw new InvalidOperationException();

        var a = int.Parse(input[ranges[0]]);
        var b = int.Parse(input[ranges[1]]);

        return (a, b);
    }


    //// * Summary *

    //BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
    //13th Gen Intel Core i9 - 13950HX 2.20GHz, 1 CPU, 32 logical and 24 physical cores
    //    .NET SDK 11.0.100-preview.4.26230.115
    //[Host]            : .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86 - 64 - v3[AttachedDebugger]
    //LongRun-.NET 10.0 : .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86 - 64 - v3

    //    Job=LongRun-.NET 10.0  Runtime=.NET 10.0  IterationCount=100
    //LaunchCount=3  WarmupCount=15

    //| Method | input   | Mean     | Error    | StdDev    | Median   | Gen0   | Allocated |
    //|------- |-------- |---------:|---------:|----------:|---------:|-------:|----------:|
    //| A      | 123,987 | 40.35 ns | 2.100 ns | 10.737 ns | 33.89 ns | 0.0055 |     104 B |
    //| B      | 123,987 | 17.33 ns | 0.888 ns |  4.376 ns | 19.91 ns |      - |         - |
    //| C      | 123,987 | 25.87 ns | 0.248 ns |  1.236 ns | 25.94 ns |      - |         - |

}
