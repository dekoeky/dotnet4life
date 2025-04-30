using BenchmarkDotNet.Attributes;
using SharedLibrary.Reflection.StaticGeneric.Models;

namespace PerformanceTests.Benchmarks.Reflection.StaticGeneric;

[ShortRunJob]
[MemoryDiagnoser]
public class StaticGenericInterfaceMethodTests
{

    //| Method                                       | Mean      | Error     | StdDev    | Median    | Allocated |
    //|--------------------------------------------- |----------:|----------:|----------:|----------:|----------:|
    //| TimesTwoImplicit_DirectCall                  | 0.0287 ns | 0.4795 ns | 0.0263 ns | 0.0344 ns |         - |
    //| TimesTwoImplicit_Dispatcher                  | 0.0649 ns | 1.2582 ns | 0.0690 ns | 0.0446 ns |         - |
    //| TimesTwoImplicit_DispatcherAggressiveInlined | 0.0213 ns | 0.2930 ns | 0.0161 ns | 0.0142 ns |         - |
    //| TimesTwoImplicit_ReflectedDelegate           | 1.2809 ns | 1.7840 ns | 0.0978 ns | 1.3249 ns |         - |
    //| TimesTwoImplicit_ReflectedFunc               | 1.1430 ns | 0.5426 ns | 0.0297 ns | 1.1388 ns |         - |
    //| TimesTwo_Dispatcher                          | 0.0025 ns | 0.0793 ns | 0.0043 ns | 0.0000 ns |         - |
    //| TimesTwo_DispatcherAggressiveInlined         | 0.0346 ns | 0.1775 ns | 0.0097 ns | 0.0310 ns |         - |
    //| TimesTwo_ReflectedDelegate                   | 1.1481 ns | 0.7477 ns | 0.0410 ns | 1.1352 ns |         - |
    //| TimesTwo_ReflectedFunc                       | 1.2010 ns | 0.4998 ns | 0.0274 ns | 1.1870 ns |         - |

    [Params(10)]
    public double Value;

    //Performing the reflection a single time, and timing only the call to the resulting delegate
    private static readonly DoubleOperationDelegate TimesTwoImplicitDelegate = DelegateHelper.CreateDelegate<TimesTwoImplicit>();
    private static readonly Func<double, double> TimesTwoImplicitFunc = DelegateHelper.CreateFunc<TimesTwoImplicit>();
    private static readonly DoubleOperationDelegate TimesTwoDelegate = DelegateHelper.CreateDelegate<TimesTwo>();
    private static readonly Func<double, double> TimesTwoFunc = DelegateHelper.CreateFunc<TimesTwo>();

    [Benchmark] public double TimesTwoImplicit_DirectCall() => TimesTwoImplicit.Execute(Value);
    [Benchmark] public double TimesTwoImplicit_Dispatcher() => DispatchHelper.Execute<TimesTwoImplicit>(Value);
    [Benchmark] public double TimesTwoImplicit_DispatcherAggressiveInlined() => DispatchHelper.ExecuteAggressiveInlined<TimesTwoImplicit>(Value);
    [Benchmark] public double TimesTwoImplicit_ReflectedDelegate() => TimesTwoImplicitDelegate.Invoke(Value);
    [Benchmark] public double TimesTwoImplicit_ReflectedFunc() => TimesTwoImplicitFunc.Invoke(Value);

    //[Benchmark] public double TimesTwo_DirectCall() => TimesTwo.Execute(Value); // Not possible, because the method is explicitly implemented
    [Benchmark] public double TimesTwo_Dispatcher() => DispatchHelper.Execute<TimesTwo>(Value);
    [Benchmark] public double TimesTwo_DispatcherAggressiveInlined() => DispatchHelper.ExecuteAggressiveInlined<TimesTwo>(Value);
    [Benchmark] public double TimesTwo_ReflectedDelegate() => TimesTwoDelegate.Invoke(Value);
    [Benchmark] public double TimesTwo_ReflectedFunc() => TimesTwoFunc.Invoke(Value);
}