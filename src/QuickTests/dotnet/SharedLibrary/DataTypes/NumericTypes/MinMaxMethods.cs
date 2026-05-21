using System.Numerics;

namespace SharedLibrary.DataTypes.NumericTypes;

/// <summary>
/// Demonstrates different methods to find min/max values of data.
/// </summary>
public static class MinMaxMethods
{
    /// <summary>
    /// A type to be used for demonstration purposes.
    /// </summary>
    public readonly record struct DemoPoint(int X, int Y, int Z);

    public static (int minX, int maxX, int minY, int maxY) MinMaxXY_ViaLoop(DemoPoint[] points)
    {
        var (minX, maxX) = (int.MaxValue, int.MinValue);
        var (minY, maxY) = (int.MaxValue, int.MinValue);

        for (var i = 0; i < points.Length; i++)
        {
            if (points[i].X < minX) minX = points[i].X;
            if (points[i].Y < minY) minY = points[i].Y;
            if (points[i].X > maxX) maxX = points[i].X;
            if (points[i].Y > maxY) maxY = points[i].Y;
        }

        return (minX, maxX, minY, maxY);
    }

    public static (int minX, int maxX, int minY, int maxY, int minZ, int maxZ)
        MinMaxXYZ_ViaLoop(DemoPoint[] points)
    {
        var (minX, maxX) = (int.MaxValue, int.MinValue);
        var (minY, maxY) = (int.MaxValue, int.MinValue);
        var (minZ, maxZ) = (int.MaxValue, int.MinValue);


        for (var i = 0; i < points.Length; i++)
        {
            if (points[i].X < minX) minX = points[i].X;
            if (points[i].Y < minY) minY = points[i].Y;
            if (points[i].Z < minZ) minZ = points[i].Z;
            if (points[i].X > maxX) maxX = points[i].X;
            if (points[i].Y > maxY) maxY = points[i].Y;
            if (points[i].Z > maxZ) maxZ = points[i].Z;
        }

        return (minX, maxX, minY, maxY, minZ, maxZ);
    }

    public static (int minX, int maxX, int minY, int maxY) MinMaxXY_ViaLinq(DemoPoint[] points)
    {
        var minX = points.Min(p => p.X);
        var maxX = points.Max(p => p.X);
        var minY = points.Min(p => p.Y);
        var maxY = points.Max(p => p.Y);

        return (minX, maxX, minY, maxY);
    }

    public static (int minX, int maxX, int minY, int maxY, int minZ, int maxZ)
        MinMaxXYZ_ViaLinq(DemoPoint[] points)
    {
        var minX = points.Min(p => p.X);
        var maxX = points.Max(p => p.X);
        var minY = points.Min(p => p.Y);
        var maxY = points.Max(p => p.Y);
        var minZ = points.Min(p => p.Z);
        var maxZ = points.Max(p => p.Z);

        return (minX, maxX, minY, maxY, minZ, maxZ);
    }


    public static (TIn min, TIn max) GenericMinMax<TIn>(TIn[] data)
        where TIn : struct, IMinMaxValue<TIn>
        , IEqualityOperators<TIn, TIn, bool>
        , IComparisonOperators<TIn, TIn, bool>
        => GenericMinMax(data, e => e);

    public static (TOut min, TOut max) GenericMinMax<TIn, TOut>(TIn[] data, Func<TIn, TOut> predicate)
        where TOut : struct, IMinMaxValue<TOut>
        , IEqualityOperators<TOut, TOut, bool>
        , IComparisonOperators<TOut, TOut, bool>
    {
        (TOut Min, TOut Max) result = (TOut.MaxValue, TOut.MinValue);

        for (var i = 0; i < data.Length; i++)
        {
            var element = data[i];

            var value = predicate(element);
            if (value < result.Min) result.Min = value;
            if (value > result.Max) result.Max = value;
        }

        return result;
    }

    public static (TOut min, TOut max)[] GenericMinMax<TIn, TOut>(TIn[] data, params Func<TIn, TOut>[] predicates)
        where TOut : struct, IMinMaxValue<TOut>
        , IEqualityOperators<TOut, TOut, bool>
        , IComparisonOperators<TOut, TOut, bool>
    {
        var results = new (TOut min, TOut max)[predicates.Length];

        for (var i = 0; i < data.Length; i++)
        {
            results[i].min = TOut.MaxValue;
            results[i].max = TOut.MinValue;
        }

        for (var i = 0; i < data.Length; i++)
        {
            var element = data[i];
            for (var j = 0; j < predicates.Length; j++)
            {
                var value = predicates[j](element);
                if (value < results[j].min) results[j].min = value;
                if (value > results[j].max) results[j].max = value;
            }
        }

        return results;
    }
}