using System.Globalization;

namespace MultiTargetTests;

[TestClass]
public sealed class FloatTests
{
    [TestMethod]
    [DataRow(1.2345679f)] // This value has (small) different results between for example .NET9 and .NET FrameWork 480
    [DataRow(0.1f)]
    public void ToString(float value)
    {
        // ---------- ARRANGE ----------
        string?[] formats = [null, "G", "G7", "G9", "R"];
        IFormatProvider formatProvider = CultureInfo.InvariantCulture;

        // ---------- ACT --------------
        var results = formats.ToDictionary(f => f ?? "null", f => value.ToString(f, formatProvider));
        results.Add("bits", BitsOf(value).PadLeft(32, '0'));

        // ---------- ASSERT -----------
        foreach (var kv in results)
        {
            var format = kv.Key;
            var s = kv.Value;
            Console.WriteLine($"[{CompileTimeConstants.TFM,7}] ({format,6}) {s}");
        }

        // ---------------------------
        // Example Outputs
        // ---------------------------

        // [ net481] (  null) 1.234568
        // [ net481] (     G) 1.234568
        // [ net481] (    G7) 1.234568
        // [ net481] (    G9) 1.23456788
        // [ net481] (     R) 1.23456788
        // [ net481] (  bits) 11111111110011110000001100101001000000000000000000000000000000

        // [ net9.0] (  null) 1.2345679
        // [ net9.0] (     G) 1.2345679
        // [ net9.0] (    G7) 1.234568
        // [ net9.0] (    G9) 1.23456788
        // [ net9.0] (     R) 1.2345679
        // [ net9.0] (  bits) 11111111110011110000001100101001000000000000000000000000000000

        // Conclusion:
        //      Bits are always equal
        //      null, G, G9, R interpretations are different
        //      G7 interpretation is equal
    }

    /// <summary>
    /// A (framework-independent) conversion of a float to a (32-)bit string.
    /// </summary>
    private static string BitsOf(float value)
    {
        var bits = BitConverter.DoubleToInt64Bits(value);
        return Convert.ToString(bits, 2).PadLeft(32, '0');
    }
}