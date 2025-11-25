using System.Diagnostics;

namespace QuickTests.DataTypes.Strings;

[TestClass]
public class StringTests
{
    [TestMethod]
    [DataRow(null, true)]
    [DataRow("Hello World", false)]
    [DataRow("", true)]
    [DataRow("  ", false)]
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
}