namespace QuickTests.DataTypes.Numeric_Types;

[TestClass]
public class UInt16Tests : NumericTestsBase<ushort>
{
    [DataTestMethod]
    [DataRow((ushort)0u)]
    [DataRow((ushort)1u)]
    [DataRow((ushort)11u)]
    [DataRow((ushort)255u)]
    [DataRow((ushort)256u)]
    [DataRow((ushort)0b1000_0000_0000_0000)] // MSB
    [DataRow((ushort)0xFFFF)]
    public void Explain(ushort value)
    {
        var bitString = Convert.ToString(value, 2).PadLeft(16, '0');
        var bytes = BitConverter.GetBytes(value); // length = 2
        var hex = $"0x{value:x4}";
        var (label0, label1) = BitConverter.IsLittleEndian
            ? ("LSB", "MSB")
            : ("MSB", "LSB");

        Console.WriteLine($"Original Value:        {value,10} ({value.GetType().Name})");
        Console.WriteLine();
        Console.WriteLine("Binary:");
        Console.WriteLine($"  Bits:                {bitString}");
        Console.WriteLine($"  Hex:                 {hex}");
        Console.WriteLine($"  Bytes:");
        Console.WriteLine($"    [0]:               {bytes[0],-3} ({label0})");
        Console.WriteLine($"    [1]:               {bytes[1],-3} ({label1})");
        Console.WriteLine();
        Console.WriteLine("Contribution of each bit:");

        // Loop 16 bits (0..15)
        uint u = value;
        for (var i = 0; i < 16; i++)
        {
            var mask = 1u << i;
            var isSet = (u & mask) != 0;
            var bit = isSet ? '1' : '0';
            var contrib = isSet ? mask : 0u;
            Console.WriteLine($"  Bit [{i:D2}] = {bit} x {mask,5} = {contrib,5}");
        }
    }
}