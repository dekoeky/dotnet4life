using BenchmarkDotNet.Attributes;

namespace PerformanceTests.Benchmarks.DataTypes.DateTimes;

[MemoryDiagnoser]
public class DateTimeNowTests
{
    [Benchmark]
    public DateTime Now() => DateTime.Now;

    [Benchmark]
    public DateTime UtcNow() => DateTime.UtcNow;
}