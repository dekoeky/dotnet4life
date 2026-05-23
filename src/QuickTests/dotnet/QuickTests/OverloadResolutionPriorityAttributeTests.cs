using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace QuickTests;

[TestClass]

/// <summary>
/// <see cref="OverloadResolutionPriorityAttribute"/> related tests.
/// </summary>
public class OverloadResolutionPriorityAttributeTests
{
    private const short int16 = 16;
    private const int int32 = 32;
    private const long int64 = 64;

    [TestMethod]
    public void Demo()
    {
        Debug.WriteLine($"No Attribute:");
        Debug.WriteLine($"int16 -> {NoAttribute(int16)}");
        Debug.WriteLine($"int32 -> {NoAttribute(int32)}");
        Debug.WriteLine($"int64 -> {NoAttribute(int64)}");
        Debug.WriteLine();


        Debug.WriteLine($"Prioritized (highest) Int64 -> Int32 -> Int16 (lowest):");
        Debug.WriteLine($"int16 -> {PrioritizedLongToShort(int16)}");
        Debug.WriteLine($"int32 -> {PrioritizedLongToShort(int32)}");
        Debug.WriteLine($"int64 -> {PrioritizedLongToShort(int64)}");
        Debug.WriteLine();


        Debug.WriteLine($"Prioritized (highest) Int16 -> Int32 -> Int64 (lowest):");
        Debug.WriteLine($"int16 -> {PrioritizedShortToLong(int16)}");
        Debug.WriteLine($"int32 -> {PrioritizedShortToLong(int32)}");
        Debug.WriteLine($"int64 -> {PrioritizedShortToLong(int64)}");
        Debug.WriteLine();


        Debug.WriteLine($"Prioritized (highest) Int16 -> Int32 -> Int64 (lowest):");
        Debug.WriteLine($"int16 -> {A(int16)}");
        Debug.WriteLine($"int32 -> {A(int32)}");
        Debug.WriteLine($"int64 -> {A(int64)}");
        Debug.WriteLine();
    }


    public static Type NoAttribute(short value) => value.GetType();

    public static Type NoAttribute(int value) => value.GetType();

    public static Type NoAttribute(long value) => value.GetType();



    [OverloadResolutionPriority(1)] // Lowest
    public static Type PrioritizedLongToShort(short value) => value.GetType();

    [OverloadResolutionPriority(2)] // Middle
    public static Type PrioritizedLongToShort(int value) => value.GetType();

    [OverloadResolutionPriority(3)] // Highest
    public static Type PrioritizedLongToShort(long value) => value.GetType();



    [OverloadResolutionPriority(3)] // Highest
    public static Type PrioritizedShortToLong(short value) => value.GetType();

    [OverloadResolutionPriority(2)] // Middle
    public static Type PrioritizedShortToLong(int value) => value.GetType();

    [OverloadResolutionPriority(1)] // Lowest
    public static Type PrioritizedShortToLong(long value) => value.GetType();
}
