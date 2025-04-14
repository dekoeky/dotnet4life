namespace QuickTests.DataTypes.Enums.BaseTypes;

[TestClass]
public class FlagTests
{
    [DataTestMethod]
    [DataRow(Permissions.None, Permissions.None)]
    [DataRow(Permissions.Read, Permissions.None)]
    [DataRow(Permissions.Read | Permissions.None, Permissions.None)]
    public void HasFlag_OnZeroValueFlag(Permissions value, Permissions flag)
    {
        //Act
        var flagSet = value.HasFlag(flag);

        //Assert
        Console.WriteLine($"Value:    {value}");
        Console.WriteLine($"Flag:     {flag}");
        Console.WriteLine($"Flag Set: {flagSet}");
    }


    [Flags]
    public enum Permissions
    {
        None = 0,
        Read = 1,
        Write = 2,
        Execute = 4
    }
}