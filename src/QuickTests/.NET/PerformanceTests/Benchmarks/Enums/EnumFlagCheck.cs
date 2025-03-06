using BenchmarkDotNet.Attributes;

namespace PerformanceTests.Benchmarks.Enums;

[ShortRunJob]
//[SimpleJob]
//[Config(typeof())]
public class EnumFlagCheck
{
    [Benchmark]
    [ArgumentsSource(nameof(TestData))]
    public bool Old(Status status, Status flagToCheck, bool _) => status.HasFlag(flagToCheck);

    [Benchmark]
    [ArgumentsSource(nameof(TestData))]
    public bool New(Status status, Status flagToCheck, bool _) => (status & flagToCheck) == flagToCheck;




    [Flags]
    public enum Status : byte
    {
        PICTURE_ACTIVE = 0x01,
        AUTODIMMING_ACTIVE = 0x02,
        RESTARTED = 0x04,
        COMMUNICATION_TIMEOUT = 0x08,
        UPS_ACTIVE = 0x10,
        MANUAL_CONTROL = 0x20,
        CLIMATE_WARNING = 0x40,
        BUSY = 0x80
    }

    public static IEnumerable<object[]> TestData() =>
    [
        // Status,  Flag to check,  expected result

        [Status.BUSY, Status.BUSY, true],
        [Status.BUSY, Status.CLIMATE_WARNING, false],
        [Status.BUSY | Status.CLIMATE_WARNING, Status.BUSY, true ],
        [Status.BUSY | Status.CLIMATE_WARNING, Status.CLIMATE_WARNING, true ],
    ];
}