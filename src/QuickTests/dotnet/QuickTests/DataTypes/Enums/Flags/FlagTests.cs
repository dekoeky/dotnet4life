#pragma warning disable MSTEST0032
namespace QuickTests.DataTypes.Enums.Flags;

[TestClass]
public class FlagTests
{
    [TestMethod]
    public void DefaultValueIsNone()
    {
        //Act
        const Permissions value = default;

        //Assert
        Assert.AreEqual(Permissions.None, value);
    }

    [DataTestMethod]
    [TestForAllEnumValues]
    public void HasFlagForZeroFlag_Always_True(Permissions value)
    {
        //Act
        var result = value.HasFlag(Permissions.None /* which is 0 */);

        //Assert
        Assert.IsTrue(result);
    }

    [DataTestMethod]
    [PermissionsHasFlagTestData]
    public void IsFlagSet_New(Permissions value, Permissions flagToCheck, bool expectedResult)
    {
        //Act
        var result = value.HasFlag(flagToCheck);

        //Assert
        Console.WriteLine($"Testing if HasFlag on {value.GetType().Name} value returns {expectedResult} for flag {flagToCheck}");
        Console.WriteLine($"Value:           {value} ({(int)value})");
        Console.WriteLine($"Flag:            {flagToCheck}");
        Console.WriteLine($"Expected Result: {expectedResult}");
        Console.WriteLine($"Result:          {result}");
        Assert.AreEqual(expectedResult, result);
    }

    [DataTestMethod]
    [PermissionsHasFlagTestData]
    public void IsFlagSet_Old(Permissions value, Permissions flagToCheck, bool expectedResult)
    {
        //Act
        var result = (value & flagToCheck) == flagToCheck;

        //Assert
        Console.WriteLine($"Testing if HasFlag on {value.GetType().Name} value returns {expectedResult} for flag {flagToCheck}");
        Console.WriteLine($"Value:           {value} ({(int)value})");
        Console.WriteLine($"Flag:            {flagToCheck}");
        Console.WriteLine($"Expected Result: {expectedResult}");
        Console.WriteLine($"Result:          {result}");
        Assert.AreEqual(expectedResult, result);
    }
}