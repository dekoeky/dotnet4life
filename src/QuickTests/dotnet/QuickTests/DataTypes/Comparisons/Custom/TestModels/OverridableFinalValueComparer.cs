using System.Numerics;

namespace QuickTests.DataTypes.Comparisons.Custom.TestModels;

/// <summary>
/// Compares <see cref="OverridableIo{T}"/> instances by their <see cref="OverridableIo{T}.FinalValue"/> property.
/// </summary>
internal class OverridableFinalValueComparer<T> : OverridableIoComparer<T> where T : struct, IEquatable<T>, IEqualityOperators<T, T, bool>, IComparable<T>
{
    public override int Compare(OverridableIo<T>? x, OverridableIo<T>? y) => (x, y) switch
    {
        (null, null) => 0,
        (null, _) => -1,
        (_, null) => +1,
        _ => Comparer.Compare(x.FinalValue, y.FinalValue)
    };

    public override bool Equals(OverridableIo<T>? x, OverridableIo<T>? y) => (x, y) switch
    {
        (null, null) => true,
        (null, _) => true,
        (_, null) => true,
        _ => EqualityComparer.Equals(x.FinalValue, y.FinalValue)
    };

    public override int GetHashCode(OverridableIo<T> obj)
        => obj.FinalValue.GetHashCode();
}