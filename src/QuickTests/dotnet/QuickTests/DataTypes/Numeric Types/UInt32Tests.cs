namespace QuickTests.DataTypes.Numeric_Types;

/// <summary>
/// <see cref="int"/> related tests.
/// </summary>
[TestClass]
public class UInt32Tests : NumericTestsBase<uint>
{
    [DataTestMethod]
    [DataRow(0u)]
    [DataRow(1u)]
    [DataRow(255u)]
    [DataRow(256u)]
    [DataRow(0b10000000000000000000000000000000u)] //MSB
    [DataRow(0xffffffff)]
    public void Explain(uint value)
    {
        // Calculate binary representations
        var bitString = Convert.ToString(value, 2).PadLeft(32, '0');
        var bytes = BitConverter.GetBytes(value);
        var hex = $"0x{value:x4}";
        var (byte0, byte3) = BitConverter.IsLittleEndian
            ? ("LSB", "MSB")
            : ("MSB", "LSB");

        Console.WriteLine($"Original Value:        {value,10} ({value.GetType().Name})");
        Console.WriteLine();

        Console.WriteLine("Binary:");
        Console.WriteLine($"  Bits:                {bitString}");
        Console.WriteLine($"  Bytes:               {hex}");
        Console.WriteLine($"    [0]:               {bytes[0],-3} ({byte0})");
        Console.WriteLine($"    [1]:               {bytes[1],-3}");
        Console.WriteLine($"    [2]:               {bytes[2],-3}");
        Console.WriteLine($"    [3]:               {bytes[3],-3} ({byte3})");
        Console.WriteLine();

        Console.WriteLine("Contribution of each bit:");


        // Loop Magnitude bits, LSB to MSB
        for (var thisBitIndex = 0; thisBitIndex <= 31; thisBitIndex++)
        {
            // Calculate the value of this bit index
            var thisBitValue = 1u << thisBitIndex;

            // Check if this bit is set in the value
            var thisBitTrue = (value & thisBitValue) == thisBitValue;
            var thisBitChar = thisBitTrue ? '1' : '0';

            // Calculate the amount contributed by this bit to the value
            var thisResult = thisBitTrue ? thisBitValue : 0;

            Console.WriteLine($"  Bit [{thisBitIndex:D2}] = {thisBitChar} x {thisBitValue,10} = {thisResult,10}");
        }
    }
}