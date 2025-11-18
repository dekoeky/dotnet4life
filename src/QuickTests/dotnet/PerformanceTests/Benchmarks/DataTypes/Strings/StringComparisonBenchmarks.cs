using BenchmarkDotNet.Attributes;
// ReSharper disable UnassignedField.Global
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace PerformanceTests.Benchmarks.DataTypes.Strings;

[ShortRunJob]
[MemoryDiagnoser]
public class StringComparisonBenchmarks
{
    private const string TestValue = "Hello World";

    [Params(null, "", TestValue)]
    public string? NullableString;

    [Params(TestValue)]
    public string NonNullableString;


    [Benchmark]
    public bool NullCheck_And_Nullable_Equals_NonNullable() => NullableString is not null && NullableString.Equals(NonNullableString);

    [Benchmark]
    public bool NonNullable_Equals_Nullable() => NonNullableString.Equals(NullableString);

    [Benchmark]
    public bool EqualityOperator() => NonNullableString == NullableString;
}
