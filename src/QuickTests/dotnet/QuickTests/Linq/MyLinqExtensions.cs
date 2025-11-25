namespace QuickTests.Linq;

/// <summary>
/// Custom Linq Extensions.
/// </summary>
/// <seealso cref="System.Linq.Enumerable"/>
internal static class MyLinqExtensions
{
    public static (TSource? min, TSource? max) MinMaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        where TKey : notnull
        => MinMaxBy(source, keySelector, null);

    public static (TSource? min, TSource? max) MinMaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey>? comparer)
        where TKey : notnull

    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(keySelector);

        comparer ??= Comparer<TKey>.Default;

        using var e = source.GetEnumerator();

        if (!e.MoveNext())
        {
            return default(TSource) is null
                ? (default, default)
                : throw new Exception("No elements");
        }

        var minValue = e.Current;
        var maxValue = minValue;
        var minKey = keySelector(minValue);
        var maxKey = minKey;

        while (e.MoveNext())
        {
            var nextValue = e.Current;
            var nextKey = keySelector(nextValue);

            if (comparer.Compare(nextKey, maxKey) > 0)
            {
                maxKey = nextKey;
                maxValue = nextValue;
            }


            if (comparer.Compare(nextKey, minKey) < 0)
            {
                minKey = nextKey;
                minValue = nextValue;
            }
        }

        return (minValue, maxValue);
    }
}