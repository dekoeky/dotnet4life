using BenchmarkDotNet.Attributes;

namespace PerformanceTests.Benchmarks;

public class DateTimeNowTests
{
    [Benchmark]
    public DateTime Now() => DateTime.Now;

    [Benchmark]
    public DateTime UtcNow() => DateTime.UtcNow;
}