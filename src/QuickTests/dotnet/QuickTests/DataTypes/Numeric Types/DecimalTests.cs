namespace QuickTests.DataTypes.Numeric_Types;

/// <summary>
/// <see cref="double"/> related tests.
/// </summary>
/// <seealso href="https://learn.microsoft.com/en-us/dotnet/fundamentals/runtime-libraries/system-decimal"/>
[TestClass]
public class DecimalTests : NumericTestsBase<decimal>
{
    [TestMethod] public void MaxValue() => Explain(decimal.MaxValue);
    [TestMethod] public void MinValue() => Explain(decimal.MinValue);


    [TestMethod]
    public void Show_Bits()
    {
        //Arrange
        decimal[] values =
        [
            0M,
            3.14M,
            -3.14M,
            +1000M,
            -1000M,
        ];

        //Assert
        foreach (var value in values)
            Explain(value);
    }


    [TestMethod]
    public void Calculations()
    {
        //Arrange
        const decimal startValue = 10.1M;
        const int howManyTimesDivideByTwo = 4;
        var value = startValue;

        for (var i = 0; i < howManyTimesDivideByTwo; i++)
        {
            //ASSERT
            //Print Current Value
            Explain(value);

            //ACT
            //Divide the value by 2, to see what happens to the Scale
            value /= 2;
        }
    }

    /// <summary>
    /// Centralized way to demonstrate the internals of a given <see cref="decimal"/> value.
    /// </summary>
    /// <param name="value"></param>
    private static void Explain(decimal value)
    {
        var hundredTwentyEightBits = decimal.GetBits(value);

        // 96-bit integer parts
        var low = hundredTwentyEightBits[0];
        var mid = hundredTwentyEightBits[1];
        var high = hundredTwentyEightBits[2];

        // Flags: bits[3]
        var flags = hundredTwentyEightBits[3];
        var scale = (flags >> 16) & 0xFF; // bits 16–23
        var isNegative = (flags & unchecked((int)0x80000000)) != 0;

        Console.WriteLine($"Decimal Value: {value}");
        Console.WriteLine($"Low 32 bits  : {Convert.ToString(low, 2).PadLeft(32, '0')}");
        Console.WriteLine($"Mid 32 bits  : {Convert.ToString(mid, 2).PadLeft(32, '0')}");
        Console.WriteLine($"High 32 bits : {Convert.ToString(high, 2).PadLeft(32, '0')}");
        Console.WriteLine($"Scale        : {scale}");
        Console.WriteLine($"Sign         : {(isNegative ? '-' : '+')}");

        Console.WriteLine("Bits          :");

        foreach (var thirtyTwoBits in hundredTwentyEightBits)
            Console.WriteLine($"{thirtyTwoBits:b32}");

        //Separation between values from different calls
        Console.WriteLine();
        Console.WriteLine();
    }
}