namespace QuickTests.DataTypes;

public static class TupleExtensions
{
    public static object?[] ToObjectArray<T1, T2, T3>(this ValueTuple<T1, T2, T3> tuple)
    {
        return [tuple.Item1, tuple.Item2, tuple.Item3];
    }
}