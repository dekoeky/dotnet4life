using System.Numerics;
using System.Text.Json;

namespace QuickTests.Json.Converters.Generic.ValueTimesTwo.Tests;

[TestClass]
public class ValueTimesTwoConverterViaOptionsTests
{
    private static readonly JsonSerializerOptions Options = new()
    {
        Converters =
        {
            new ValueTimesTwoConverterFactory()
        }
    };

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
        var json = JsonSerializer.Serialize(data, Options);

        //Assert
        Console.WriteLine(json);
    }

    [TestMethod]
    public void Deserialize2()
    {
        //Arrange
        const string json = """{"Value":30,"FloatingValue":6.283185307179586}""";

        //Act
        var data = JsonSerializer.Deserialize<Data<int, double>>(json, Options);

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
    private class Data<TA, TB>
        where TA : INumber<TA>
        where TB : INumber<TB>
    {
        public required TA Value { get; set; }

        public required TB FloatingValue { get; set; }
    }
}