namespace PerformanceTests.Benchmarks.ExtensionMethods.Types;

public static class MyExtensions
{
    public static int GetSumInterface(this IMyInterface item) => item.NumberA + item.NumberB;
    public static int GetSumGeneric<T>(this T item) where T : IMyInterface => item.NumberA + item.NumberB;
}