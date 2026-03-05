namespace QuickTests.CheckedVsUnchecked;

/// <summary>
/// <c>checked</c> vs <c>unchecked</c> keyword tests.
/// </summary>
[TestClass]
public class CheckedVsUncheckedByteTests
{
    [TestMethod]
    [TestProperty("context", "unchecked")]
    [DataRow((ushort)255, (byte)255)]
    [DataRow((ushort)256, (byte)0)]
    [DataRow((ushort)257, (byte)1)]
    [DataRow((ushort)(69 + 256), (byte)69)]
    public void UShort_CastTo_Byte_Unchecked(ushort value, byte expected)
    {
        // ARRANGE
        byte result;

        //ACT
        unchecked
        {
            result = (byte)value;
        }

        //ASSERT
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [TestProperty("context", "checked")]
    [DataRow((ushort)255)]
    [DataRow((ushort)256)]
    [DataRow((ushort)257)]
    [DataRow((ushort)(69 + 256))]
    public void UShort_CastTo_Byte_Checked(ushort value)
    {
        //ASSERT
        Assert.ThrowsExactly<OverflowException>(() =>
        {

            //ACT
            checked
            {
                var result = (byte)value;
                Console.WriteLine($"Result: {result}");
            }
        });
    }

    [TestMethod]
    [TestProperty("context", "checked")]
    public void ByteMaxValue_PlusOne_Checked_ThrowsOverflowException()
    {
        // ARRANGE
        var value = byte.MaxValue;

        // ASSERT
        Assert.ThrowsExactly<OverflowException>(() =>
        {
            // ACT
            checked
            {
                value++;
            }
        });

        // Never reached:
        Console.WriteLine($"Result: {value}");
    }

    [TestMethod]
    [TestProperty("context", "unchecked")]
    public void ByteMaxValue_PlusOne_Unchecked_ResultsIn0()
    {
        // ARRANGE
        const byte expected = byte.MinValue;
        var value = byte.MaxValue;

        // ACT
        unchecked
        {
            value++;
        }

        // ASSERT
        Assert.AreEqual(expected, value);
    }
}