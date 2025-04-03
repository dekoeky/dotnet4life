namespace QuickTests.DataTypes;

/// <summary>
/// <see cref="Random"/> extension methods.
/// </summary>
public static class RandomExtensions
{
    public static TimeSpan NextTimespan(this Random random, TimeSpan from, TimeSpan until)
    {
        return new TimeSpan(random.NextInt64(from.Ticks, until.Ticks));
    }
}