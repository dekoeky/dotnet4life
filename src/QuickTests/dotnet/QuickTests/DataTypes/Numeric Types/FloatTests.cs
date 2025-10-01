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
        Console.WriteLine($"Fixed Point (G):  {value:G}");
        Console.WriteLine($"Fixed Point (g2): {value:g2}");
        Console.WriteLine($"Fixed Point (g3): {value:g3}");
        Console.WriteLine($"Fixed Point (g4): {value:g4}");
        Console.WriteLine($"Fixed Point (g5): {value:g5}");
        Console.WriteLine($"Fixed Point (g6): {value:g6}");
        Console.WriteLine($"Fixed Point (g7): {value:g7}");
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