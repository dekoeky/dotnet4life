using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace QuickTests.DataTypes.Strings;

[TestClass]
public class StringTests
{
    [TestMethod]
    [DataRow(null, true)]
    [DataRow("Hello World", false)]
    [DataRow("", true)]
    [DataRow("  ", false)]
    [DataRow("\t", false)]
    [DataRow("\r", false)]
    [DataRow("\n", false)]
    [DataRow("\r\n", false)]
    public void IsNullOrEmpty(string? s, bool expected)
    {
        // ---------- ARRANGE ----------
        var printableString = s is null ? "(null)" : $"'{s}'";

        // ---------- ACT --------------
        var nullOrEmpty = string.IsNullOrEmpty(s);

        // ---------- ASSERT -----------
        Debug.WriteLine($"String {printableString,20}: Null Or Empty: {nullOrEmpty}");
        Assert.AreEqual(expected, nullOrEmpty);
    }

    [TestMethod]
    [DataRow(null, true)]
    [DataRow("Hello World", false)]
    [DataRow("", true)]
    [DataRow("  ", true)]
    [DataRow("\t", true)]
    [DataRow("\r", true)]
    [DataRow("\n", true)]
    [DataRow("\r\n", true)]
    [DataRow("\r\nHelloWorld", false)]
    public void IsNullOrWhiteSpace(string? s, bool expected)
    {
        // ---------- ARRANGE ----------
        var printableString = s is null ? "(null)" : $"'{s}'";

        // ---------- ACT --------------
        var nullOrEmpty = string.IsNullOrWhiteSpace(s);

        // ---------- ASSERT -----------
        Debug.WriteLine($"String {printableString,20}: Null Or WhiteSpace: {nullOrEmpty}");
    }

    [TestMethod]
    [SuppressMessage("ReSharper", "ConvertToConstant.Local", Justification = "For clarity")]
    public void StringInterning()
    {
        // Arrange
        var a = "hello";
        var b = "hello"; // This will NOT be a new reference/instance, due to string interning

        // Assert
        Assert.AreEqual(a, b);
        Assert.AreSame(a, b);
    }

    [TestMethod]
    [SuppressMessage("ReSharper", "ConvertToConstant.Local", Justification = "For clarity")]
    public void StringInterningWorkAround()
    {
        // Arrange
        var a = "hello";
        var b = new string("hello"); // Force new string (new reference), with same value

        // Assert
        Assert.AreEqual(a, b);
        Assert.AreNotSame(a, b);
    }
}