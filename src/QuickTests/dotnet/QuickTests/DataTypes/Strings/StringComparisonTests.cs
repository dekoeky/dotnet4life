namespace QuickTests.DataTypes.Strings;

[TestClass]
public class StringComparisonTests
{
    [TestMethod]
    [DataRow("Hello World", "Hello World", StringComparison.CurrentCulture, true)]
    public void SimpleExample(string a, string b, StringComparison comparison, bool expectedEqual)
    {
        //Arrange
        var comparer = StringComparer.FromComparison(comparison);

        //Act
        var actuallyEqual = comparer.Equals(a, b);

        //Assert
        Assert.AreEqual(expectedEqual, actuallyEqual);
    }

    /// <summary>
    /// Demonstrate that calling <see cref="string.Equals(string?)"/>
    /// on a null string throws a <see cref="NullReferenceException"/>.
    /// </summary>
    /// <remarks><see cref="EqualityOperatorOnNullStringDoesNotThrow"/> for an alternative solution.</remarks>
    [TestMethod]
    public void EqualsOnNullStringThrowsNullReferenceException()
    {
        //Arrange
        const string? a = null;
        const string b = "SomePredeterminedValue";
        const bool expectedEqual = false;

        //Act
        Assert.Throws<NullReferenceException>(() =>
        {
            // Dereference of a possibly null reference.
            // More Info: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/nullable-warnings#possible-dereference-of-null
#pragma warning disable CS8602 
            var actual = a.Equals(b); // No StringComparison used here for simplicity
#pragma warning restore CS8602

            //Assert
            Assert.AreEqual(expectedEqual, actual);
        });

    }

    [TestMethod]
    [DynamicData(nameof(NullableStringVsNonNullableStringTestData))]
    public void EqualityOperatorOnNullStringDoesNotThrow(string? a, string b, StringComparison comparison, bool expectedEqual)
    {
        //Act
        var actuallyEqual = a == b; // The equality operator is overloaded to hangle null values gracefully.

        //Assert
        Assert.AreEqual(expectedEqual, actuallyEqual);
    }

    public static IEnumerable<object?[]> NullableStringVsNonNullableStringTestData()
    {
        foreach (var comparison in Enum.GetValues<StringComparison>())
        {
            // Test cases should return the same result for each string comparison
            yield return [null, "Hello World", comparison, false];
            yield return ["", "Hello World", comparison, false];
            yield return ["Hello World", "Hello World", comparison, true];
        }
    }
}
