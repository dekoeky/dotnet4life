namespace PerformanceTests.Benchmarks.ExtensionMethods;

public static class MyExtensions
{
    public static int GetSumInterface(this IMyInterface item) => item.Number1 + item.Number2;
    public static int GetSumGeneric<T>(this IMyInterface item) => item.Number1 + item.Number2;
}