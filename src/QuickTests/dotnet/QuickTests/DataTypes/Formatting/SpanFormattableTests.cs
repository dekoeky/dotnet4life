using SharedLibrary.DataTypes.Formatting;

namespace QuickTests.DataTypes.Formatting;

/// <summary>
/// <see cref="ISpanFormattable"/> related tests.
/// </summary>
[TestClass]
public class SpanFormattableTests
{
    [TestMethod]
    public void ToStringTest()
    {
        //Arrange
        var example = new ExampleFormattable();

        //Act
        var result = example.ToString();

        //Assert
        Console.WriteLine(result);
        Assert.IsFalse(string.IsNullOrEmpty(result));
        Assert.IsFalse(string.IsNullOrWhiteSpace(result));
    }

    [TestMethod]
    public void ToStringWithFormatTest()
    {
        //Arrange
        var example = new ExampleFormattable();

        //Act
        var result = example.ToString("DEMO", null);

        //Assert
        Console.WriteLine(result);
        Assert.IsFalse(string.IsNullOrEmpty(result));
        Assert.IsFalse(string.IsNullOrWhiteSpace(result));
    }

    [TestMethod]
    public void TryFormatTest()
    {
        //Arrange
        var example = new ExampleFormattable();
        const string format = "DEMO";
        Span<char> x = stackalloc char[200];

        //Act
        var success = example.TryFormat(x, out var charsWritten, format, null);
        var result = new string(x[..charsWritten]);

        //Assert
        Console.WriteLine(result);
        Assert.IsTrue(success);
        Assert.IsFalse(string.IsNullOrEmpty(result));
        Assert.IsFalse(string.IsNullOrWhiteSpace(result));
    }


    [TestMethod]
    public void StringInterpolation()
    {
        //Arrange
        var example = new ExampleFormattable();

        //Act
        var result = $"{example}";

        //Assert
        Console.WriteLine(result);
        Assert.IsFalse(string.IsNullOrEmpty(result));
        Assert.IsFalse(string.IsNullOrWhiteSpace(result));
    }

    [TestMethod]
    public void StringFormat()
    {
        //Arrange
        var example = new ExampleFormattable();

        //Act
        // ReSharper disable once UseStringInterpolation
        var result = string.Format("{0}", example);

        //Assert
        Console.WriteLine(result);
        Assert.IsFalse(string.IsNullOrEmpty(result));
        Assert.IsFalse(string.IsNullOrWhiteSpace(result));
    }
}