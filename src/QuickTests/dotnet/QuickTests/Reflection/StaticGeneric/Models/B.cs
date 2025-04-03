namespace QuickTests.Reflection.StaticGeneric.Models;

internal class B : IDemo<B, A>
{
    static double IDemo<B, A>.TimesTwo(double value)
    {
        Console.WriteLine($"Calling method {nameof(IDemo<B, A>.TimesTwo)} of interface {typeof(IDemo<B, A>).GetFriendlyTypeName()} on type {nameof(B)}");
        return value * 2;
    }
}