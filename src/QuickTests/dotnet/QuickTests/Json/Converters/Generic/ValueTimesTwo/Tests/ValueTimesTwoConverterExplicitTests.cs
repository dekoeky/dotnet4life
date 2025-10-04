using System.Text.Json;
using System.Text.Json.Serialization;

namespace QuickTests.Json.Converters.Generic.ValueTimesTwo.Tests;

/// <summary>
/// Tests generic JsonConverter <see cref="ValueTimesTwoConverter{T}"/> by explicitly defining it on properties
/// </summary>
[TestClass]
public class ValueTimesTwoConverterExplicitTests
{
    [TestMethod]
    public void Serialize()
    {
        //Arrange
        var data = new Data
        {
            IntegerValue = 15,
            FloatingValue = Math.PI
        };

        //Act
        var json = JsonSerializer.Serialize(data);

        //Assert
        Console.WriteLine(json);
    }

    [TestMethod]
    public void Deserialize()
    {
        //Arrange
        const string json = """
                            {
                                "IntegerValue":30,
                                "FloatingValue":6.283185307179586
                            }
                            """;

        //Act
        var data = JsonSerializer.Deserialize<Data>(json);

        //Assert
        Assert.IsNotNull(data);
        Assert.AreEqual(15, data.IntegerValue);
        Console.WriteLine(data.IntegerValue);
    }

    private class Data
    {
        [JsonConverter(typeof(ValueTimesTwoConverter<int>))]
        public int IntegerValue { get; init; }

        [JsonConverter(typeof(ValueTimesTwoConverter<double>))]
        public double FloatingValue { get; init; }
    }
}