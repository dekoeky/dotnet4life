using BenchmarkDotNet.Attributes;
using System.Diagnostics.CodeAnalysis;

namespace PerformanceTests.Benchmarks.DataTypes.Strings;

[ShortRunJob]
//[SimpleJob]
//[Config(typeof())]
[MemoryDiagnoser]
public class StringParameterBenchmarks
{
    private const string TableNameConstant = "site_metadata";
    [SuppressMessage("ReSharper", "ConvertToConstant.Local")]
    private static readonly string TableNameStatic = TableNameConstant;
    private static ReadOnlySpan<char> TableNameStaticSpan => TableNameConstant;

    [Benchmark] public bool FromConstString() => TestMethod(TableNameConstant);
    [Benchmark] public bool FromStaticReadonlyString() => TestMethod(TableNameStatic);
    [Benchmark] public bool FromStaticReadonlySpan() => TestMethod(TableNameStaticSpan);


    private static bool TestMethod(ReadOnlySpan<char> str) => !str.IsEmpty;
}
