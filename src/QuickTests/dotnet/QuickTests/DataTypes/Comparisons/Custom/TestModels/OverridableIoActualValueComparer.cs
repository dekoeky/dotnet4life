using System.Numerics;

namespace QuickTests.DataTypes.Comparisons.Custom.TestModels;

/// <summary>
/// Compares <see cref="OverridableIo{T}"/> instances by their <see cref="OverridableIo{T}.ActualValue"/> property.
/// </summary>
internal class OverridableIoActualValueComparer<T> : OverridableIoComparer<T> where T : struct, IEquatable<T>, IEqualityOperators<T, T, bool>, IComparable<T>
{
    public override int Compare(OverridableIo<T>? x, OverridableIo<T>? y) => (x, y) switch
    {
        (null, null) => 0,
        (null, _) => -1,
        (_, null) => +1,
        _ => Comparer.Compare(x.ActualValue, y.ActualValue)
    };

    public override bool Equals(OverridableIo<T>? x, OverridableIo<T>? y) => (x, y) switch
    {
        (null, null) => true,
        (null, _) => true,
        (_, null) => true,
        _ => EqualityComparer.Equals(x.ActualValue, y.ActualValue)
    };

    public override int GetHashCode(OverridableIo<T> obj)
        => obj.ActualValue.GetHashCode();
}