namespace QuickTests.DataTypes.Numeric_Types;

/// <summary>
/// <see cref="float"/> related tests.
/// </summary>
/// <seealso href="https://learn.microsoft.com/en-us/dotnet/fundamentals/runtime-libraries/system-float"/>
[TestClass]
public class FloatTests : NumericTestsBase<float>
{
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
}