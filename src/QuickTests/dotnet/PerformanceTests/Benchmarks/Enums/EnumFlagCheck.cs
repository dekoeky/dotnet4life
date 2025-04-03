using BenchmarkDotNet.Attributes;

namespace PerformanceTests.Benchmarks.Enums;

public class EnumFlagCheck
{
    [Benchmark]
    [ArgumentsSource(nameof(TestData))]
    public bool HasFlag(Status status, Status flagToCheck) => status.HasFlag(flagToCheck);

    [Benchmark]
    [ArgumentsSource(nameof(TestData))]
    public bool AndOperator(Status status, Status flagToCheck) => (status & flagToCheck) == flagToCheck;

    [Flags]
    public enum Status : byte
    {
        Flag1 = 0b00000001,
        Flag2 = 0b00000010,
    }

    public static IEnumerable<object[]> TestData() =>
    [
        // Status,  Flag to check
        //--------------------------
        [Status.Flag1, Status.Flag1],
        [Status.Flag2, Status.Flag2],

        [Status.Flag1, Status.Flag2],
        [Status.Flag2, Status.Flag1],

        [Status.Flag2 | Status.Flag1, Status.Flag2 ],
        [Status.Flag2 | Status.Flag1, Status.Flag1 ],
    ];
}