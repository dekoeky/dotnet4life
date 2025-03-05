using BenchmarkDotNet.Attributes;

namespace PerformanceTests.Benchmarks.DataTypes.Strings;

[ShortRunJob]
//[SimpleJob]
//[Config(typeof())]
[MemoryDiagnoser]
public class StringParameterBenchmarks
{
    private const string _tableNameConstant = "site_metadata";
    private static readonly string _tableNameStatic = _tableNameConstant;
    private static ReadOnlySpan<char> _tableNameStaticSpan => _tableNameConstant;

    [Benchmark] public bool FromConstString() => TestMethod(_tableNameConstant);
    [Benchmark] public bool FromStaticReadonlyString() => TestMethod(_tableNameStatic);
    [Benchmark] public bool FromStaticReadonlySpan() => TestMethod(_tableNameStaticSpan);


    private static bool TestMethod(ReadOnlySpan<char> str) => !str.IsEmpty;
}
