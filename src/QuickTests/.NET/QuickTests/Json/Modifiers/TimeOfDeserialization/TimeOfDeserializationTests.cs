using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace QuickTests.Json.Modifiers.TimeOfDeserialization;

[TestClass]
public class TimeOfDeserializationTests
{
    [TestMethod]
    public void DeserializedAt_CustomProperty()
    {
        //Arrange
        var options = new JsonSerializerOptions
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

        const string json = """
                            {
                                "MyName": "SomeName"
                            }
                            """; //Note that there is no MyTime property defined

        //Act
        var start = DateTime.Now;
        var data = JsonSerializer.Deserialize<SomePoco>(json, options);
        var stop = DateTime.Now;

        //Assert
        Console.WriteLine($"Start Deserialization: {start:O}");
        Console.WriteLine($"{nameof(data.MyTime)} Property, after Deserialization: {data.MyTime:O}");
        Console.WriteLine($"Stop Deserialization: {stop:O}");
    }
}