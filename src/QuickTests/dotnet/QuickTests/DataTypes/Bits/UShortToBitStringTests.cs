namespace QuickTests.DataTypes.Bits;

/// <summary>
/// Tests for converting ushort to bit strings.
/// </summary>
[TestClass]
public class UShortToBitStringTests
{
    [DataTestMethod]
    [DynamicData(nameof(TestData))]
    public void Convert_ToString(ushort value, string expectedBitString)
    {
        //Act
        var bitString = Convert.ToString(value, 2).PadLeft(16, '0');

        //Assert
        Console.WriteLine($"{value,5} -> {bitString}");
        Assert.AreEqual(expectedBitString, bitString);
    }

#if NET8_0_OR_GREATER
    [DataTestMethod]
    [DynamicData(nameof(TestData))]
    public void ToString_B(ushort value, string expectedBitString)
    {
        //Act
        var bitString = value.ToString("B");

        //Assert
        Console.WriteLine($"{value,5} -> {bitString}");
        Assert.IsTrue(expectedBitString.EndsWith(bitString));
    }

    [DataTestMethod]
    [DynamicData(nameof(TestData))]
    public void ToString_b8(ushort value, string expectedBitString)
    {
        //Act
        var bitString = value.ToString("b8");

        //Assert
        Console.WriteLine($"{value,5} -> {bitString}");
        Assert.IsTrue(expectedBitString.EndsWith(bitString));
    }

    [DataTestMethod]
    [DynamicData(nameof(TestData))]
    public void ToString_b16(ushort value, string expectedBitString)
    {
        //Act
        var bitString = value.ToString("b16");

        //Assert
        Console.WriteLine($"{value,5} -> {bitString}");
        Assert.AreEqual(expectedBitString, bitString);
    }
#endif


    public static IEnumerable<(ushort value, string bitString)> TestData
    {
        get
        {
            yield return (0, "0000000000000000");
            yield return (1, "0000000000000001");
            yield return (2, "0000000000000010");
            yield return (3, "0000000000000011");
            yield return (4, "0000000000000100");
            yield return (5, "0000000000000101");

            yield return (255, "0000000011111111");
            yield return (256, "0000000100000000");
            yield return (257, "0000000100000001");

            yield return (ushort.MaxValue, "1111111111111111");
        }
    }
}