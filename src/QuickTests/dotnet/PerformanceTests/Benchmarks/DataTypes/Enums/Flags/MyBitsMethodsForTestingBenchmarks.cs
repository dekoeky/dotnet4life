using BenchmarkDotNet.Attributes;
using SharedLibrary.DataTypes.Enums.Flags;

namespace PerformanceTests.Benchmarks.DataTypes.Enums.Flags;

/// <summary>
/// What is the fastest way to check if a flag was set on an enum type?
/// </summary>
[MemoryDiagnoser]
public class MyBitsMethodsForTestingBenchmarks
{
    [Params(MyBits.None)]
    public MyBits InitialValue;

    [Params(MyBits.Bit2)]
    public MyBits flagToSet;

    public MyBits value;

    [GlobalSetup]
    public void GlobalSetup()
    {
        value = InitialValue;
    }

    [Benchmark]
    public void SetFlag() => value = MyBitsMethodsForTesting.SetFlag(value, flagToSet);

    [Benchmark]
    public void SetFlagByRef() => MyBitsMethodsForTesting.SetFlagByRef(ref value, flagToSet);

    [Benchmark]
    public void SetFlagByRefUnsafe() => MyBitsMethodsForTesting.SetFlagByRefUnsafe(ref value, flagToSet);
}