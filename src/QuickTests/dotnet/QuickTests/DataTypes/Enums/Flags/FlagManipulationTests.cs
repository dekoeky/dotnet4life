using SharedLibrary.DataTypes.Enums.Flags;
using System.Diagnostics;

namespace QuickTests.DataTypes.Enums.Flags;

[TestClass]
public class FlagManipulationTests
{
    [DataTestMethod]
    [DataRow(MyBits.None, MyBits.Bit2, (MyBits)0b00000100)]
    [DataRow((MyBits)0b11111011, MyBits.Bit2, (MyBits)0b11111111)]
    public void SetFlag(MyBits initialValue, MyBits flagToSet, MyBits expected)
    {
        // ---------- ARRANGE ----------

        var value = initialValue;

        // ---------- ACT --------------
        value |= flagToSet;

        // ---------- ASSERT -----------
        Debug.WriteLine($"Initial Value:  {initialValue}");
        Debug.WriteLine($"Flag to Set:    {flagToSet}");
        Debug.WriteLine($"Expected Value: {expected}");
        Debug.WriteLine($"Actual Value:   {value}");
        Assert.AreEqual(expected, value);
    }

    [DataTestMethod]
    [DataRow(MyBits.All_, MyBits.Bit2, (MyBits)0b11111011)]
    [DataRow(MyBits.None, MyBits.Bit2, (MyBits)0b00000000)]
    public void ResetFlag(MyBits initialValue, MyBits flagToReset, MyBits expected)
    {
        // ---------- ARRANGE ----------
        var value = initialValue;

        // ---------- ACT --------------
        value &= ~flagToReset;

        // ---------- ASSERT -----------
        Debug.WriteLine($"Initial Value:  {initialValue}");
        Debug.WriteLine($"Flag to Reset:  {flagToReset}");
        Debug.WriteLine($"Expected Value: {expected}");
        Debug.WriteLine($"Actual Value:   {value}");
        Assert.AreEqual(expected, value);
    }
}