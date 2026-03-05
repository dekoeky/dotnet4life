namespace QuickTests.DataTypes.NumericTypes;

#pragma warning disable CS0078 // The 'l' suffix is easily confused with the digit '1'

/// <summary>
/// Demonstrates which datatypes are interpreted by default for specific 'literal suffixes'.
/// </summary>
/// <seealso href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types#integer-literals"/>
/// <seealso href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types#real-literals"/>
[TestClass]
public class LiteralSuffixes
{
    [TestMethod(DisplayName = "Literal Suffix")]
    [DataRow("10", 10, typeof(int))]
    [DataRow("10u", 10u, typeof(uint))]
    [DataRow("10U", 10U, typeof(uint))]
    [DataRow("10l", 10l, typeof(long))]
    [DataRow("10L", 10L, typeof(long))]
    [DataRow("10ul", 10ul, typeof(ulong))]
    [DataRow("10UL", 10UL, typeof(ulong))]
    [DataRow("10Ul", 10Ul, typeof(ulong))]
    [DataRow("10uL", 10uL, typeof(ulong))]
    [DataRow("10lu", 10lu, typeof(ulong))]
    [DataRow("10LU", 10LU, typeof(ulong))]
    [DataRow("10lU", 10lU, typeof(ulong))]
    [DataRow("10Lu", 10Lu, typeof(ulong))]
    [DataRow("10f", 10f, typeof(float))]
    [DataRow("10F", 10F, typeof(float))]
    [DataRow("10d", 10d, typeof(double))]
    [DataRow("10D", 10D, typeof(double))]
    public void LiteralSuffix(string literalAsString, object value, Type expectedType)
    {
        //ACT
        var actualType = value.GetType();
        Console.WriteLine($"""
                           Literal:       {literalAsString}
                           Value:         {value}
                           Type:          {actualType}
                           Expected Type: {expectedType}
                           """);

        //ASSERT
        Assert.IsInstanceOfType(value, expectedType);
    }
}