using System.Diagnostics;

namespace SharedLibrary.Reflection.StaticGeneric.Models;

public class TimesTwo : IDemo<TimesTwo, DestinationType>
{
    static double IDemo<TimesTwo, DestinationType>.Execute(double value)
    {
        Debug.WriteLine(
            $"Calling method {nameof(IDemo<TimesTwo, DestinationType>.Execute)}" +
            $" on interface {typeof(IDemo<TimesTwo, DestinationType>).GetFriendlyTypeName()}" +
            $" on type {nameof(TimesTwo)}");
        return value * 2;
    }
}