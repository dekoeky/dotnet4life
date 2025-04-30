using System.Diagnostics;

namespace SharedLibrary.Reflection.StaticGeneric.Models;

public class TimesThree : IDemo<TimesThree, DestinationType>
{
    static double IDemo<TimesThree, DestinationType>.Execute(double value)
    {
        Debug.WriteLine(
            $"Calling method {nameof(IDemo<TimesThree, DestinationType>.Execute)}" +
            $" on interface {typeof(IDemo<TimesThree, DestinationType>).GetFriendlyTypeName()}" +
            $" on type {nameof(TimesThree)}");

        return value * 3;
    }
}