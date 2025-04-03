using QuickTests.DataTypes.Comparisons.Custom.TestData;
using QuickTests.DataTypes.Comparisons.Custom.TestModels;
using System.Numerics;
using System.Text.Json;

namespace QuickTests.DataTypes.Comparisons.Custom.Tests;

[TestClass]
public class OverridableIoFinalValueComparerTests
{
    private static readonly OverridableIoComparer<int> Comparer = OverridableIoComparer<int>.FinalValue;

    private static readonly JsonSerializerOptions JsonSerializerOptions = new(JsonSerializerOptions.Default) { WriteIndented = true, };

    [TestMethod]
    public void StaticComparerIsCorrectType()
    {
        //Assert
        Assert.AreEqual(typeof(OverridableFinalValueComparer<int>), Comparer.GetType());
    }

    [DataTestMethod]
    [OverridableIoFinalValueComparerTestData]
    public void Compare(bool expectedEqual, OverridableIo<int> a, OverridableIo<int> b)
    {
        //Act
        var result = Comparer.Equals(a, b);
        // Assuming the test data is always 2 different instances, this shows that the default .Equals method returns false
        var defaultEqualsResult = a.Equals(b);

        //Assert
        PrintResult(a, b, result);
        Assert.AreEqual(expectedEqual, result);
        Assert.IsFalse(defaultEqualsResult);
    }


    private static void PrintResult<T>(OverridableIo<T> a, OverridableIo<T> b, bool result) where T
        : struct, IEquatable<T>, IEqualityOperators<T, T, bool> => Console.WriteLine(
        $"{nameof(a)}:{Environment.NewLine}" +
        JsonSerializer.Serialize(a, JsonSerializerOptions) + Environment.NewLine + Environment.NewLine +
        $"{nameof(b)}:{Environment.NewLine}" +
        JsonSerializer.Serialize(b, JsonSerializerOptions) + Environment.NewLine + Environment.NewLine +
        "where found " + (result ? "EQUAL" : "NOT EQUAL") + " by comparer" + Comparer.GetType().Name.ToUpper()
    );
}