namespace QuickTests.DataTypes;

[TestClass]
public class RandomTests
{
    private static readonly Random Random = new();

    [TestMethod]
    public void Next()
    {
        //Act
        var result = Random.Next();

        //Assert
        Console.WriteLine(result);
    }

    [TestMethod]
    public void NextDouble()
    {
        //Act
        var result = Random.NextDouble();

        //Assert
        Console.WriteLine(result);
    }

    [TestMethod]
    public void NextInt64()
    {
        //Act
        var result = Random.NextInt64();

        //Assert
        Console.WriteLine(result);
    }

    [TestMethod]
    public void NextSingle()
    {
        //Act
        var result = Random.NextSingle();

        //Assert
        Console.WriteLine(result);
    }

    [TestMethod]
    public void NextBytes()
    {
        //Arrange
        byte[] bytes = [1, 2, 3, 4, 5];

        //Act
        Random.NextBytes(bytes);

        //Assert
        Console.WriteLine(string.Join(", ", bytes));
    }

    [TestMethod]
    public void NextTimespan()
    {
        //Arrange
        var from = new TimeSpan(00, 00, 00);
        var until = new TimeSpan(24, 00, 00);
        //Act
        var result = Random.NextTimespan(from, until);

        //Assert
        Console.WriteLine(result);
    }
}