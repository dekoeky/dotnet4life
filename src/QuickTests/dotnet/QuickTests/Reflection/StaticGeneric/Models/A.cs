namespace QuickTests.Reflection.StaticGeneric.Models;

internal class A : IDemo<A, B>
{
    static double IDemo<A, B>.TimesTwo(double value)
    {
        Console.WriteLine($"Calling method {nameof(IDemo<A, B>.TimesTwo)} of interface {typeof(IDemo<A, B>).GetFriendlyTypeName()} on type {nameof(A)}");
        return value * 2;
    }
}