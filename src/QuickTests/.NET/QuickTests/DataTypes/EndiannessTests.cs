namespace QuickTests.DataTypes;

[TestClass]
public class EndiannessTests
{
    [TestMethod]
    public void IsLittleEndian()
    {

        //Act
        var isLittleEndian = BitConverter.IsLittleEndian;

        //Assert
        Assert.IsTrue(isLittleEndian);
    }
}