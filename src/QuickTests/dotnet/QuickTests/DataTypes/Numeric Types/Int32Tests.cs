namespace QuickTests.DataTypes.Numeric_Types;

/// <summary>
/// <see cref="int"/> related tests.
/// </summary>
[TestClass]
public class Int32Tests : NumericTestsBase<int>
{
    //TODO: Explicit Two's complement representation

    /// <summary>
    /// Mask to extract the sign bit from an <see cref="int"/>.
    /// </summary>
    private const uint SignMask = 0b10000000_00000000_00000000_00000000; //MSB or Leftmost bit

    [DataTestMethod]
    [DataRow(int.MinValue)]
    [DataRow(-256)]
    [DataRow(-255)]
    [DataRow(-1)]
    [DataRow(0)]
    [DataRow(1)]
    [DataRow(11)]
    [DataRow(255)]
    [DataRow(256)]
    [DataRow(65024 /* 255 * 255 - 1 */)]
    [DataRow(65025 /* 255 * 255 */)]
    [DataRow(int.MaxValue)]
    public void Explain(int value)
    {
        // Calculate binary representations
        var bitString = Convert.ToString(value, 2).PadLeft(32, '0');
        var bytes = BitConverter.GetBytes(value);
        var hex = $"0x{value:x4}";
        var (byte0, byte3) = BitConverter.IsLittleEndian
            ? ("LSB", "MSB")
            : ("MSB", "LSB");

        var signBit = (value & SignMask) == SignMask;
        var sign = signBit ? "- (Negative)" : "+ (Positive or Zero)";
        var signBitChar = signBit ? '1' : '0';
        // The magnitude is the absolute value of the number, ignoring the sign bit.
        var magnitude = value < 0
            ? (uint)-value    // Convert to positive (two's complement)
            : (uint)+value;   // Positive numbers already work as uint
        var magnitudeBitString = Convert.ToString(magnitude, 2).PadLeft(31, '0');



        Console.WriteLine($"Original Value:        {value,11} ({value.GetType().Name})");
        Console.WriteLine();

        Console.WriteLine("Binary:");
        Console.WriteLine($"  Bits:                {bitString}");
        Console.WriteLine($"  Bytes:               {hex}");
        Console.WriteLine($"    [0]:               {bytes[0],-3} ({byte0})");
        Console.WriteLine($"    [1]:               {bytes[1],-3}");
        Console.WriteLine($"    [2]:               {bytes[2],-3}");
        Console.WriteLine($"    [3]:               {bytes[3],-3} ({byte3})");
        Console.WriteLine();

        Console.WriteLine("Breakdown:");
        Console.WriteLine($"  Sign bit:            {signBitChar} (MSB or Left-Most Bit)");
        Console.WriteLine($"  Sign:                {sign}");
        Console.WriteLine($"  Magnitude:           {magnitude,-10} ({magnitudeBitString})");
        Console.WriteLine("  Contribution of each bit:");


        // Loop Magnitude bits, LSB to MSB
        for (var thisBitIndex = 0; thisBitIndex <= 31; thisBitIndex++)
        {
            // Calculate the value of this bit index
            var thisBitValue = 1u << thisBitIndex;

            // Check if this bit is set in the value
            var thisBitTrue = (magnitude & thisBitValue) == thisBitValue;
            var thisBitChar = thisBitTrue ? '1' : '0';

            // Calculate the amount contributed by this bit to the value
            var thisResult = thisBitTrue ? thisBitValue : 0;

            Console.WriteLine($"    Bit [{thisBitIndex:D2}] = {thisBitChar} x {thisBitValue,10} = {thisResult,10}");
        }
    }
}