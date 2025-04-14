namespace QuickTests.DataTypes.Enums.BaseTypes;

[TestClass]
public class EnumBaseTypeTest
{
    [Flags]
    private enum ByteEnum : byte { A = 1, B = 2 }
    [Flags]
    private enum IntEnum : int { A = 100, B = 200 }
    [Flags]
    private enum LongEnum : long { A = 1000, B = 2000 }


    [TestMethod]
    public void CastToInt()
    {
        //Act
        int i1 = (int)ByteEnum.A;
        int i2 = (int)IntEnum.A;
        int i3 = (int)LongEnum.A;

        //Assert
        Assert.AreEqual(1, i1);
        Assert.AreEqual(100, i2);
        Assert.AreEqual(1000, i3);
    }

    [TestMethod]
    public void CastFromInt()
    {
        //Arrange
        const int i1 = (int)ByteEnum.A;
        const int i2 = (int)IntEnum.A;
        const int i3 = (int)LongEnum.A;

        //Act
        const ByteEnum e1 = (ByteEnum)i1;
        const IntEnum e2 = (IntEnum)i2;
        const LongEnum e3 = (LongEnum)i3;

        //Assert
        Assert.AreEqual(ByteEnum.A, e1);
        Assert.AreEqual(IntEnum.A, e2);
        Assert.AreEqual(LongEnum.A, e3);
    }

    [DataTestMethod]
    [DataRow(typeof(ByteEnum))]
    [DataRow(typeof(IntEnum))]
    [DataRow(typeof(LongEnum))]
    public void TestEnum(Type enumType)
    {
        Assert.IsTrue(enumType.IsEnum);

        var baseType = Enum.GetUnderlyingType(enumType);
        Console.WriteLine($"Enum: {enumType.Name}, BaseType: {baseType.Name}");

        var values = Enum.GetValues(enumType);
        foreach (var val in values)
        {
            var castVal = Convert.ChangeType(val, baseType);
            Console.WriteLine($"  {val} => {castVal} ({castVal.GetType().Name})");
        }
    }
}