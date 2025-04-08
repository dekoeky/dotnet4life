using BenchmarkDotNet.Attributes;

namespace PerformanceTests.Benchmarks.DataTypes.Enums;

/// <summary>
/// What is the fastest way to invert an enum with two values.
/// </summary>
public class EnumInvert
{
    [ParamsAllValues]
    public BoolEnum Status;

    [Benchmark]
    public BoolEnum Invert_ViaIfs() => Status.Invert_ViaIfs();

    [Benchmark]
    public BoolEnum Invert_ViaBitwiseXor() => Status.Invert_ViaBitwiseXor();

    [Benchmark]
    public BoolEnum Invert_ViaSwitchExpression() => Status.Invert_ViaSwitchExpression();


    //| Method                     | Status | Mean      | Error     | StdDev    | Median    |
    //|--------------------------- |------- |----------:|----------:|----------:|----------:|
    //| Invert_ViaIfs              | Off    | 0.6058 ns | 0.0365 ns | 0.0830 ns | 0.5916 ns |
    //| Invert_ViaBitwiseXor       | Off    | 0.0013 ns | 0.0042 ns | 0.0060 ns | 0.0000 ns |
    //| Invert_ViaSwitchExpression | Off    | 0.4454 ns | 0.0333 ns | 0.0702 ns | 0.4418 ns |
    //| Invert_ViaIfs              | On     | 0.6585 ns | 0.0372 ns | 0.0833 ns | 0.6531 ns |
    //| Invert_ViaBitwiseXor       | On     | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |
    //| Invert_ViaSwitchExpression | On     | 0.3868 ns | 0.0325 ns | 0.0714 ns | 0.3752 ns |

    // [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    //| Method                     | Status | Mean      | Error     | StdDev    |
    //|--------------------------- |------- |----------:|----------:|----------:|
    //| Invert_ViaIfs              | Off    | 0.7591 ns | 0.0403 ns | 0.1189 ns |
    //| Invert_ViaBitwiseXor       | Off    | 0.6455 ns | 0.0372 ns | 0.0933 ns |
    //| Invert_ViaSwitchExpression | Off    | 1.1566 ns | 0.0477 ns | 0.1037 ns |
    //| Invert_ViaIfs              | On     | 0.8590 ns | 0.0404 ns | 0.0580 ns |
    //| Invert_ViaBitwiseXor       | On     | 0.6147 ns | 0.0359 ns | 0.0336 ns |
    //| Invert_ViaSwitchExpression | On     | 1.2290 ns | 0.0474 ns | 0.1021 ns |

    // [MethodImpl(MethodImplOptions.NoInlining)]
    //| Method                     | Status | Mean      | Error     | StdDev    |
    //|--------------------------- |------- |----------:|----------:|----------:|
    //| Invert_ViaIfs              | Off    | 0.5680 ns | 0.0348 ns | 0.0510 ns |
    //| Invert_ViaBitwiseXor       | Off    | 0.2841 ns | 0.0302 ns | 0.0479 ns |
    //| Invert_ViaSwitchExpression | Off    | 0.3781 ns | 0.0325 ns | 0.0649 ns |
    //| Invert_ViaIfs              | On     | 0.6002 ns | 0.0352 ns | 0.0312 ns |
    //| Invert_ViaBitwiseXor       | On     | 0.2894 ns | 0.0282 ns | 0.0290 ns |
    //| Invert_ViaSwitchExpression | On     | 0.3941 ns | 0.0316 ns | 0.0536 ns |
}

public enum BoolEnum : byte
{
    Off = 0,
    On = 1,
}

file static class BoolEnumExtensions
{
    public static BoolEnum Invert_ViaIfs(this BoolEnum value)
    {
        if (value == BoolEnum.Off) return BoolEnum.On;
        if (value == BoolEnum.On) return BoolEnum.Off;
        throw new InvalidOperationException();
    }

    public static BoolEnum Invert_ViaSwitchExpression(this BoolEnum value) => value switch
    {
        BoolEnum.Off => BoolEnum.On,
        BoolEnum.On => BoolEnum.Off,
        _ => throw new InvalidOperationException()
    };

    public static BoolEnum Invert_ViaBitwiseXor(this BoolEnum value) => (BoolEnum)((int)value ^ 1);
}