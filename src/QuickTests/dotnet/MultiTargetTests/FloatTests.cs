using System.Globalization;

namespace MultiTargetTests;

[TestClass]
public sealed class FloatTests
{
    [TestMethod]
    [DataRow(1.2345679f)]
    [DataRow(0.1f)]
    public void ToString(float value)
    {
        // ---------- ARRANGE ----------
        string?[] formats = [null, "G", "G7", "G9", "R"];
        IFormatProvider formatProvider = CultureInfo.InvariantCulture;

        // ---------- ACT --------------
        var results = formats.ToDictionary(f => f ?? "(null)", f => value.ToString(f, formatProvider));

        // ---------- ASSERT -----------
        foreach (var kv in results)
        {
            var format = kv.Key;
            var s = kv.Value;
            Console.WriteLine($"[{format,6}] {s}");
        }
    }
}
