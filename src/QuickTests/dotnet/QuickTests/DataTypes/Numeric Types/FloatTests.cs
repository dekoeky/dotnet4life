using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace QuickTests.DataTypes.Numeric_Types;

/// <summary>
/// <see cref="float"/> related tests.
/// </summary>
/// <seealso href="https://learn.microsoft.com/en-us/dotnet/fundamentals/runtime-libraries/system-float"/>
/// <seealso href="https://www.h-schmidt.net/FloatConverter/IEEE754.html"/>
[TestClass]
public class FloatTests : NumericTestsBase<float>
{
    //TODO: Add check to see if numbers can be represented as float, such as 123456789f --> Becomes 123456790f


    [TestMethod]
    [DataRow(360)]
    [DataRow(359.99997f)]
    public void FormattingDemo(float value)
    {
        // ---------- ACT --------------
        Console.WriteLine($"No Format ():     {value}");
        Console.WriteLine($"Round-Trip (R):   {value:R}");
        Console.WriteLine($"Exponential (E):  {value:E}");

        Console.WriteLine();
        Console.WriteLine($"Fixed Point (F):  {value:F}");
        Console.WriteLine($"Fixed Point (F1): {value:F1}");
        Console.WriteLine($"Fixed Point (F2): {value:F2}");
        Console.WriteLine($"Fixed Point (F3): {value:F3}");
        Console.WriteLine($"Fixed Point (F4): {value:F4}");
        Console.WriteLine($"Fixed Point (F5): {value:F5}");
        Console.WriteLine($"Fixed Point (F6): {value:F6}");
        Console.WriteLine($"Fixed Point (F7): {value:F7}");

        Console.WriteLine();
        Console.WriteLine($"General (G):  {value:G}");
        Console.WriteLine($"General (g2): {value:g2}");
        Console.WriteLine($"General (g3): {value:g3}");
        Console.WriteLine($"General (g4): {value:g4}");
        Console.WriteLine($"General (g5): {value:g5}");
        Console.WriteLine($"General (g6): {value:g6}");
        Console.WriteLine($"General (g7): {value:g7}");
    }


    [TestMethod]
    [DataRow(0.1f)]
    [DataRow(1.2345679f)]
    [DataRow(float.Epsilon)]
    [DataRow(float.MaxValue)]
    [DataRow(123456.987654321f)]
    [SuppressMessage("ReSharper", "RedundantStringInterpolation", Justification = "Easier Table Formatting")]
    [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator", Justification = "Exact bitwise equality required")]
    public void RoundTripDemo(float value)
    {
        // ---------- ARRANGE ----------
        var culture = CultureInfo.InvariantCulture;

        // ---------- ACT --------------
        // No format: Same as 'G'
        var toString = value.ToString(null, culture);

        // https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings#general-format-specifier-g
        // 'G' Format meaning:
        // - dotnet (core) 2+: print the shortest round-trippable string for this value
        // - dotnet framework: G7 https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings#general-format-specifier-g
        var toStringG = value.ToString("G", culture);

        // Default format, on .NET Framework
        var toStringG7 = value.ToString("G7", culture);

        //Equal to R format, and also more performant
        var toStringG9 = value.ToString("G9", culture);
        var toStringR = value.ToString("R", culture);

        //Parse each formatted string back to a float value
        var parsedValue = float.Parse(toString, culture);
        var parsedValueG = float.Parse(toStringG, culture);
        var parsedValueG7 = float.Parse(toStringG7, culture);
        var parsedValueG9 = float.Parse(toStringG9, culture);
        var parsedValueR = float.Parse(toStringR, culture);

        //Check each round trip, whether it caused the exact same value
        var wasRoundTrippable = parsedValue == value;
        var wasRoundTrippableG = parsedValueG == value;
        var wasRoundTrippableG7 = parsedValueG7 == value;
        var wasRoundTrippableG9 = parsedValueG9 == value;
        var wasRoundTrippableR = parsedValueR == value;

        // ---------- ASSERT -----------
        Console.WriteLine($"Format  Result");
        Console.WriteLine($"(null)  {toString}");
        Console.WriteLine($"G       {toStringG}");
        Console.WriteLine($"G7      {toStringG7}");
        Console.WriteLine($"G9      {toStringG9}");
        Console.WriteLine($"R       {toStringR}");

        Console.WriteLine();
        Console.WriteLine($"Format  Result");
        Console.WriteLine($"(null)  {wasRoundTrippable}");
        Console.WriteLine($"G       {wasRoundTrippableG}");
        Console.WriteLine($"G7      {wasRoundTrippableG7}");
        Console.WriteLine($"G9      {wasRoundTrippableG9}");
        Console.WriteLine($"R       {wasRoundTrippableR}");
    }

    [DataTestMethod]
    [DataRow(+MathF.PI)]
    [DataRow(-MathF.PI)]
    [DataRow(float.MaxValue)]
    [DataRow(float.MinValue)]
    [DataRow(float.Epsilon)]
    [DataRow(+99999.0f)]
    [DataRow(-99999.0f)]
    [DataRow(0f)]
    [DataRow(11f)]
    public void Float_ToSignExponentMantissa_AndBack(float value)
    {
        var bits = BitConverter.SingleToInt32Bits(value);
        var bitString = Convert.ToString(bits, 2).PadLeft(32, '0');

        // Extract sign (bit 31, leftmost bit)
        var sign = (bits >> 31) == 0 ? 1 : -1;

        // Extract exponent (bits 23-30)
        var exponent = (int)((bits >> 23) & 0b11111111);
        var actualExponent = exponent - 127; // IEEE 754 bias for floats

        // Extract fraction (mantissa) (23 bits)
        var fractionBits = bits & 0x7FFFFF; // Mask 23 bits

        var mantissa = 0.0f;

        if (value != 0)
        {
            // Convert fraction to decimal (normalized form has an implicit leading 1)
            mantissa = 1.0f; // Implicit 1 for normalized numbers
            for (var i = 0; i < 23; i++)
            {
                if ((fractionBits & (1 << (22 - i))) != 0)
                    mantissa += (float)Math.Pow(2, -(i + 1));
            }
        }

        // Compute the final value manually
        var reconstructedValue = sign * mantissa * (float)Math.Pow(2, actualExponent);

        Console.WriteLine($"Original Value:         {value}");
        Console.WriteLine($"As bits:                {bitString}");
        Console.WriteLine();
        Console.WriteLine($"Sign:                   {sign:+#;-#;0}");
        Console.WriteLine($"Exponent (Raw):         {exponent}");
        Console.WriteLine($"Exponent (Actual):      {actualExponent}");
        Console.WriteLine($"Mantissa:               {mantissa}");
        Console.WriteLine();
        Console.WriteLine($"Reconstructed Value:    {reconstructedValue}");
    }

    [TestMethod]
    [DataRow(float.MinValue, -3.4028233E+38f)]
    [DataRow(-10f, -9.999999f)]
    [DataRow(-1000000f, -999999.94f)]
    [DataRow(0f, float.Epsilon)]
    [DataRow(10f, 10.000001f)]
    [DataRow(1000000f, 1000000.06f)]
    [DataRow(float.MaxValue, float.PositiveInfinity)]
    public void BitIncrement(float original, float expected)
    {
        // ---------- ACT --------------
        var result = MathF.BitIncrement(original);
        var delta = result - original;

        // ---------- ASSERT -----------
        Assert.AreEqual(expected, result);
        Console.WriteLine($"""
                           Original (float):                        {original}
                           Smallest value, larger than original:    {result}
                           Delta:                                   {delta}
                           """);
    }

    [TestMethod]
    [DataRow(float.MinValue, float.NegativeInfinity)]
    [DataRow(-10f, -10.000001f)]
    [DataRow(-1000000f, -1000000.06f)]
    [DataRow(0f, -float.Epsilon)]
    [DataRow(10f, 9.999999f)]
    [DataRow(1000000f, 999999.94f)]
    [DataRow(float.MaxValue, 3.4028233E+38f)]
    public void BitDecrement(float original, float expected)
    {
        // ---------- ACT --------------
        var result = MathF.BitDecrement(original);
        var delta = result - original;

        // ---------- ASSERT -----------
        Assert.AreEqual(expected, result);
        Console.WriteLine($"""
                           Original (float):                        {original}
                           Smallest value, larger than original:    {result}
                           Delta:                                   {delta}
                           """);
    }
}