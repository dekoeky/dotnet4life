using System.Diagnostics;

namespace SharedLibrary.Reflection.StaticGeneric.Models;

/// <summary>
/// This type implements the <see cref="IDemo{TSelf,TOther}"/> interface IMPLICITLY.
/// </summary>
public class TimesTwoImplicit : IDemo<TimesTwoImplicit, DestinationType>
{
    public static double Execute(double value)
    {
        Debug.WriteLine(
            $"Calling method {nameof(Execute)}" +
            $" on type {nameof(TimesTwoImplicit)}");
        return value * 2;
    }
}