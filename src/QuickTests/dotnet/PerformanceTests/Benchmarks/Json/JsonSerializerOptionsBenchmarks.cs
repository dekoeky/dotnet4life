using BenchmarkDotNet.Attributes;
using System.Text.Json;

namespace PerformanceTests.Benchmarks.Json;

[SimpleJob]
[MemoryDiagnoser]
public class JsonSerializerOptionsBenchmarks
{
    // * Summary *

    //BenchmarkDotNet v0.15.0, Windows 11 (10.0.22631.5335/23H2/2023Update/SunValley3)
    //13th Gen Intel Core i9-13950HX 2.20GHz, 1 CPU, 32 logical and 24 physical cores
    //    .NET SDK 10.0.100-preview.5.25277.114
    //[Host]     : .NET 9.0.6 (9.0.625.26613), X64 RyuJIT AVX2[AttachedDebugger]
    //DefaultJob : .NET 9.0.6 (9.0.625.26613), X64 RyuJIT AVX2

    //| Method                             | Mean     | Error   | StdDev   | Gen0   | Allocated |
    //|----------------------------------- |---------:|--------:|---------:|-------:|----------:|
    //| New                                | 250.1 ns | 5.65 ns | 16.38 ns | 0.0091 |     201 B |
    //| New_JsonSerializerDefaults_General | 252.4 ns | 5.27 ns | 15.36 ns | 0.0091 |     201 B |
    //| New_JsonSerializerDefaults_Web     | 247.7 ns | 4.95 ns | 13.30 ns | 0.0091 |     201 B |
    //| New_JsonSerializerOptions_Default  | 249.7 ns | 5.12 ns | 14.70 ns | 0.0091 |     201 B |
    //| New_JsonSerializerOptions_Web      | 257.1 ns | 7.04 ns | 20.54 ns | 0.0091 |     201 B |

    [Benchmark] public JsonSerializerOptions New() => new();
    [Benchmark] public JsonSerializerOptions New_JsonSerializerDefaults_General() => new(JsonSerializerDefaults.General);
    [Benchmark] public JsonSerializerOptions New_JsonSerializerDefaults_Web() => new(JsonSerializerDefaults.Web);
    [Benchmark] public JsonSerializerOptions New_JsonSerializerOptions_Default() => new(JsonSerializerOptions.Default);
    [Benchmark] public JsonSerializerOptions New_JsonSerializerOptions_Web() => new(JsonSerializerOptions.Web);
}
