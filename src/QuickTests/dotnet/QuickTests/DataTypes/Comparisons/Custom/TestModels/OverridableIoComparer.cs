using System.Collections;
using System.Numerics;

namespace QuickTests.DataTypes.Comparisons.Custom.TestModels;

public abstract class OverridableIoComparer<T> : IComparer, IEqualityComparer, IComparer<OverridableIo<T>?>, IEqualityComparer<OverridableIo<T>?> where T : struct, IEquatable<T>, IEqualityOperators<T, T, bool>, IComparable<T>
{
    public static OverridableIoComparer<T> FinalValue => new OverridableFinalValueComparer<T>();
    public static OverridableIoComparer<T> ActualValue => new OverridableIoActualValueComparer<T>();

    protected readonly Comparer<T> Comparer = Comparer<T>.Default;
    protected readonly EqualityComparer<T> EqualityComparer = EqualityComparer<T>.Default;
    public abstract int Compare(OverridableIo<T>? x, OverridableIo<T>? y);
    public abstract bool Equals(OverridableIo<T>? x, OverridableIo<T>? y);
    public abstract int GetHashCode(OverridableIo<T> obj);


    public int Compare(object? x, object? y)
    {
        if (x == y) return 0;
        if (x == null) return -1;
        if (y == null) return 1;

        if (x is OverridableIo<T> sa && y is OverridableIo<T> sb)
            return Compare(sa, sb);

        throw new InvalidOperationException();
    }

    public new bool Equals(object? x, object? y)
    {
        if (x == y) return true;
        if (x == null || y == null) return false;

        if (x is OverridableIo<T> sa && y is OverridableIo<T> sb)
            return Equals(sa, sb);

        return x.Equals(y);
    }

    public int GetHashCode(object obj)
    {
        if (obj is OverridableIo<T> o)
            return GetHashCode(o);

        return obj.GetHashCode();
    }
}