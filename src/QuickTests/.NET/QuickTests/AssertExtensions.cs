using System.Numerics;

namespace QuickTests;

internal static class AssertExtensions
{
    public static void IsBetween<T>(this Assert assert, T min, T max, T actual, bool includeBounds) where T : IComparisonOperators<T, T, bool>
    {
        if (max < min) throw new ArgumentOutOfRangeException(nameof(max), $"{nameof(max)} should not be smaller than {nameof(min)}");
        if (includeBounds)
        {
            assert.IsLargerThanOrEqual(min, actual);
            assert.IsSmallerThanOrEqual(max, actual);
        }
        else
        {
            assert.IsLargerThan(min, actual);
            assert.IsSmallerThan(max, actual);
        }
    }

    public static void IsBetween(this Assert assert, DateTime min, DateTime max, DateTime actual, bool includeBounds)
    {
        if (max < min) throw new ArgumentOutOfRangeException(nameof(max), $"{nameof(max)} should not be smaller than {nameof(min)}");
        if (includeBounds)
        {
            assert.IsLargerThanOrEqual(min, actual);
            assert.IsSmallerThanOrEqual(max, actual);
        }
        else
        {
            assert.IsLargerThan(min, actual);
            assert.IsSmallerThan(max, actual);
        }
    }
    public static void IsLargerThan<T>(this Assert assert, T min, T actual) where T : IComparisonOperators<T, T, bool>
    {
        Assert.IsTrue(actual > min, $"The {nameof(actual)} value {actual} was not larger than {nameof(min)} value {min}");
    }
    public static void IsLargerThanOrEqual<T>(this Assert assert, T min, T actual) where T : IComparisonOperators<T, T, bool>
    {
        Assert.IsTrue(actual >= min, $"The {nameof(actual)} value {actual} was not larger than or equal to {nameof(min)} value {min}");
    }
    public static void IsSmallerThan<T>(this Assert assert, T min, T actual) where T : IComparisonOperators<T, T, bool>
    {
        Assert.IsTrue(actual < min, $"The {nameof(actual)} value {actual} was not smaller than {nameof(min)} value {min}");
    }
    public static void IsSmallerThanOrEqual<T>(this Assert assert, T min, T actual) where T : IComparisonOperators<T, T, bool>
    {
        Assert.IsTrue(actual <= min, $"The {nameof(actual)} value {actual} was not smaller than or equal to {nameof(min)} value {min}");
    }

    public static void IsLargerThan(this Assert assert, DateTime min, DateTime actual)
    {
        Assert.IsTrue(actual > min, $"The {nameof(actual)} value {actual} was not larger than {nameof(min)} value {min}");
    }
    public static void IsLargerThanOrEqual(this Assert assert, DateTime min, DateTime actual)
    {
        Assert.IsTrue(actual >= min, $"The {nameof(actual)} value {actual} was not larger than or equal to {nameof(min)} value {min}");
    }
    public static void IsSmallerThan(this Assert assert, DateTime min, DateTime actual)
    {
        Assert.IsTrue(actual < min, $"The {nameof(actual)} value {actual} was not smaller than {nameof(min)} value {min}");
    }
    public static void IsSmallerThanOrEqual(this Assert assert, DateTime min, DateTime actual)
    {
        Assert.IsTrue(actual <= min, $"The {nameof(actual)} value {actual} was not smaller than or equal to {nameof(min)} value {min}");
    }
}