using System.Numerics;

namespace QuickTests.DataTypes.Numeric_Types;

/// <summary>
/// Demonstrates the differences between regular and generic math functions.
/// </summary>
public static class GenericMathTestMethods
{
    public static double CircleAreaDouble(double r) => 2 * Math.PI * r;
    public static float CircleAreaFloat(float r) => 2 * MathF.PI * r;
    public static T CircleAreaGeneric<T>(T r)
        where T : INumber<T>, IFloatingPointConstants<T>
        => T.CreateChecked(2) * T.Pi * r;
    public static T CircleAreaGenericOptimized<T>(T r)
        where T : INumber<T>, IFloatingPointConstants<T>
        => GenericMathTestMethodsOptimized<T>.TwoPi * r;
}