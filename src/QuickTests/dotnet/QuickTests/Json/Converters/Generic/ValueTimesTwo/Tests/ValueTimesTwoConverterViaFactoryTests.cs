using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace QuickTests.Json.Converters.Generic.ValueTimesTwo.Tests;

[TestClass]
public class ValueTimesTwoConverterViaFactoryTests
{

    [TestMethod]
    public void Serialize()
    {
        //Arrange
        var data = new Data<int, double>
        {
            Value = 15,
            FloatingValue = Math.PI,
        };

        //Act
        var json = JsonSerializer.Serialize(data);

        //Assert
        Console.WriteLine(json);
    }

    [TestMethod]
    public void Deserialize2()
    {
        //Arrange
        const string json = """{"Value":30,"FloatingValue":6.283185307179586}""";

        //Act
        var data = JsonSerializer.Deserialize<Data<int, double>>(json);

        //Assert
        Assert.IsNotNull(data);
        Assert.AreEqual(15, data.Value);
        Assert.AreEqual(15, data.Value);
        Console.WriteLine(data.Value);
        Console.WriteLine(data.FloatingValue);
    }

    /// <summary>
    /// A generic poco, that defines the JsonConverter attribute, without Generic Parameters (implicit)
    /// </summary>
    private class Data<Ta, Tb>
        where Ta : INumber<Ta>
        where Tb : INumber<Tb>
    {
        [JsonConverter(typeof(ValueTimesTwoConverterFactory))]
        public required Ta Value { get; init; }

        [JsonConverter(typeof(ValueTimesTwoConverterFactory))]
        public required Tb FloatingValue { get; init; }
    }
}