namespace RequestLocalization;

public static class RandomExtensions
{
    public static T Next<T>(this Random random, ICollection<T> items)
    {
        ArgumentOutOfRangeException.ThrowIfZero(items.Count, $"{nameof(items)}.{nameof(items.Count)}");
        return items.ElementAt(random.Next(items.Count));
    }
    public static T? NextOrDefault<T>(this Random random, ICollection<T>? items)
    {
        return items is null || items.Count == 0 ? default : items.ElementAt(random.Next(items.Count));
    }
}