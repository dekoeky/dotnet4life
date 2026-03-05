namespace QuickTests.DataTypes.NumericTypes;

[TestClass]
public class NumericCasts
{
    [TestMethod]
    [DataRow(ushort.MinValue)]
    [DataRow(ushort.MaxValue)]
    [DataRow((ushort)5)]
    [DataRow((ushort)4999)]
    public void TestMethod1(ushort original)
    {
        //Act
        var castedToInt = (int)original;
        var castedBack = (ushort)castedToInt;

        //Assert
        Console.WriteLine($"""
                           Original:      {original}
                           Casted as int: {castedToInt}
                           Casted Back:   {castedBack}
                           """);
        Assert.AreEqual(original, castedBack);
    }
}