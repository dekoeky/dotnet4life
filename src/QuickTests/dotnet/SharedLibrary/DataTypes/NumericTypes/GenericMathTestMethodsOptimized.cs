using System.Numerics;

namespace SharedLibrary.DataTypes.NumericTypes;

public static class GenericMathTestMethodsOptimized<T>
        where T : INumber<T>, IFloatingPointConstants<T>
{
    /// <summary>
    /// Buffered 2xPi value for speed.
    /// </summary>
    internal static readonly T TwoPi = T.CreateChecked(2) * T.Pi;

    public static T CircleAreaGeneric(T r)
      => TwoPi * r;
}