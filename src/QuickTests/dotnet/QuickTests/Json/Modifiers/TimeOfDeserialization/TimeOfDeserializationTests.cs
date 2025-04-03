using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace QuickTests.Json.Modifiers.TimeOfDeserialization;

[TestClass]
public class TimeOfDeserializationTests
{
    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        TypeInfoResolver = new DefaultJsonTypeInfoResolver
        {
            Modifiers =
            {
                MyModifiers.ApplyDeserializationTime<SomePoco>((poco, deserializationTime) =>
                {
                    poco.MyTime = deserializationTime;
                })
            }
        }
    };

    [TestMethod]
    public void DeserializedAt_CustomProperty()
    {
        //Arrange
        const string json = """
                            {
                                "MyName": "SomeName"
                            }
                            """; //Note that there is no MyTime property defined

        //Act
        var start = DateTime.Now;
        var data = JsonSerializer.Deserialize<SomePoco>(json, _options);
        var stop = DateTime.Now;

        //Assert
        Assert.IsNotNull(data);
        Assert.That.IsBetween(start, stop, data.MyTime, true);
        Console.WriteLine($"Start Deserialization: {start:O}");
        Console.WriteLine($"{nameof(data.MyTime)} Property, after Deserialization: {data.MyTime:O}");
        Console.WriteLine($"Stop Deserialization: {stop:O}");
    }
}