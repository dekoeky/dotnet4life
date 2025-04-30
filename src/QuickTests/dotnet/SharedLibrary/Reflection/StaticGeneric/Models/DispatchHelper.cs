using System.Runtime.CompilerServices;

namespace SharedLibrary.Reflection.StaticGeneric.Models;

/// <summary>
/// Because EXPLICITLY implemented static interface method implementations can only be called on the type itself,
/// or via generic type constraints (which we use here)
/// </summary>
public static class DispatchHelper
{
    public static double Execute<T>(double value) where T : IDemo<T, DestinationType> => T.Execute(value);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ExecuteAggressiveInlined<T>(double value) where T : IDemo<T, DestinationType> => T.Execute(value);
}