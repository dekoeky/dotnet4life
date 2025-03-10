namespace QuickTests.DataTypes.Strings;

[TestClass]
public class StringInterpolationTests
{
    [TestMethod]
    public void SimpleExample()
    {
        //Arrange
        var now = DateTime.Now;

        //Act
        var result = $"Today is {now:O}";

        //Assert
        Console.WriteLine(result);
        Assert.IsFalse(string.IsNullOrEmpty(result));
    }
}