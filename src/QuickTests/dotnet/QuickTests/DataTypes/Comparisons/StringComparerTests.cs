namespace QuickTests.DataTypes.Comparisons;

[TestClass]
public class StringComparerTests
{
    [DataTestMethod]
    [DataRow("aaa", "AAA", false)]
    [DataRow("aaa", "aaa", true)]
    public void CurrentCulture(string a, string b, bool expected)
    {
        //Arrange
        var comparer = StringComparer.CurrentCulture;

        //Act
        var result = comparer.Equals(a, b);

        //Assert
        Assert.AreEqual(expected, result);
    }

    [DataTestMethod]
    [DataRow("aaa", "AAA", true)]
    [DataRow("aaa", "aaa", true)]
    public void CurrentCultureIgnoreCase(string a, string b, bool expected)
    {
        //Arrange
        var comparer = StringComparer.CurrentCultureIgnoreCase;

        //Act
        var result = comparer.Equals(a, b);

        //Assert
        Assert.AreEqual(expected, result);
    }
}