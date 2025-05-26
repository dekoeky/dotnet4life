namespace QuickTests.DataTypes.Numeric_Types;

/// <summary>
/// <see cref="double"/> related tests.
/// </summary>
/// <seealso href="https://learn.microsoft.com/en-us/dotnet/fundamentals/runtime-libraries/system-double"/>
[TestClass]
public class DoubleTests : NumericTestsBase<double>
{
    [DataTestMethod]
    [DataRow(+0.0001)]
    [DataRow(-0.0001)]
    [DataRow(+240.0)]
    [DataRow(-240.0)]
    [DataRow(+Math.PI)]
    [DataRow(-Math.PI)]
    [DataRow(double.MaxValue)]
    [DataRow(double.MinValue)]
    [DataRow(double.Epsilon)]
    [DataRow(+99999.0)]
    [DataRow(-99999.0)]
    [DataRow(0)]
    public void Double_ToSignExponentMantissa_AndBack(double value)
    {
        var bits = BitConverter.DoubleToInt64Bits(value);
        var bitString = Convert.ToString(bits, 2).PadLeft(64, '0');

        // Extract sign (bit 63, or left most bit)
        var sign = bits >> 63 == 0 ? 1 : -1;

        // Extract exponent (bits 52-62)
        var exponent = (int)((bits >> 52) & 0b11111111111);
        var actualExponent = exponent - 1023; // IEEE 754 bias

        // Extract fraction (mantissa) (52 bits)
        var fractionBits = bits & 0xFFFFFFFFFFFFF; // Mask 52 bits

        var mantissa = 0.0;

        if (value != 0)
        {
            // Convert fraction to decimal (normalized form has an implicit leading 1)
            mantissa = 1.0; // Implicit 1 for normalized numbers
            for (var i = 0; i < 52; i++)
            {
                if ((fractionBits & (1L << (51 - i))) != 0)
                    mantissa += Math.Pow(2, -(i + 1));
            }
        }

        // Compute the final value manually
        var reconstructedValue = sign * mantissa * Math.Pow(2, actualExponent);

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