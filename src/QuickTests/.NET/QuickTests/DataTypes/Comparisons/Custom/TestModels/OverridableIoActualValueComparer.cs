using System.Numerics;

namespace QuickTests.DataTypes.Comparisons.Custom.TestModels;

/// <summary>
/// Compares <see cref="OverridableIo{T}"/> instances by their <see cref="OverridableIo{T}.ActualValue"/> property.
/// </summary>
internal class OverridableIoActualValueComparer<T> : OverridableIoComparer<T> where T : struct, IEquatable<T>, IEqualityOperators<T, T, bool>, IComparable<T>
{
    public override int Compare(OverridableIo<T>? x, OverridableIo<T>? y)
    {
        if (x is null && y is null) return 0;
        if (x is null) return -1;
        if (y is null) return +1;
        return Comparer.Compare(x.ActualValue, y.ActualValue);
    }

    public override bool Equals(OverridableIo<T>? x, OverridableIo<T>? y)
    {
        if (x is null && y is null) return true;
        if (x is null) return true;
        if (y is null) return true;

        return EqualityComparer.Equals(x.ActualValue, y.ActualValue);
    }

    public override int GetHashCode(OverridableIo<T> obj)
        => obj.ActualValue.GetHashCode();
}