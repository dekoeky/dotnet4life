using BenchmarkDotNet.Attributes;

namespace PerformanceTests.Benchmarks;

[ShortRunJob]
//[SimpleJob]
//[Config(typeof())]
public class StringParameterTests
{
    private const string _tableNameConstant = "site_metadata";
    private static readonly string _tableNameStatic = _tableNameConstant;
    private static ReadOnlySpan<char> _tableNameStaticSpan => _tableNameConstant;

    [Benchmark] public bool A() => TestMethod(_tableNameConstant);
    [Benchmark] public bool B() => TestMethod(_tableNameStatic);
    [Benchmark] public bool C() => TestMethod(_tableNameStaticSpan);




    static bool TestMethod(ReadOnlySpan<char> str)
    {
        return !str.IsEmpty;
    }

}
