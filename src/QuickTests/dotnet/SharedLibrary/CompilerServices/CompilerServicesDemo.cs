using System.Runtime.CompilerServices;

namespace SharedLibrary.CompilerServices;

/// <summary>
/// Methods to demonstrate different <see cref="MethodImplOptions"/> options.
/// </summary>
public static class CompilerServicesDemo
{
    public static double Regular(double r)
        => 4.0 / 3.0 * Math.PI * r * r * r;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double AggressiveInlining(double r)
        => 4.0 / 3.0 * Math.PI * r * r * r;

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public static double AggressiveOptimization(double r)
        => 4.0 / 3.0 * Math.PI * r * r * r;

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static double NoInlining(double r)
        => 4.0 / 3.0 * Math.PI * r * r * r;

    [MethodImpl(MethodImplOptions.NoOptimization)]
    public static double NoOptimization(double r)
        => 4.0 / 3.0 * Math.PI * r * r * r;
}