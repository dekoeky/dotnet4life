using System.Text.Json;
using System.Text.Json.Serialization;

namespace QuickTests.Json.Basics.Attributes;

[TestClass]
public class JsonStringEnumMemberNameTests
{
    private readonly JsonSerializerOptions _options = new()
    {
        //For readability
        WriteIndented = true,
    };

    [TestMethod]
    public void Serialize()
    {
        //Arrange
        IoDefinition[] definitions =
        [
            new (){ Type = IoType.DigitalInput,  Address = 10, Bit = 0 },
            new (){ Type = IoType.DigitalOutput, Address = 20, Bit = 0 },
            new (){ Type = IoType.AnalogInput,   Address = 10, Bit = 1 },
            new (){ Type = IoType.AnalogOutput,  Address = 20, Bit = 1 },
        ];
        //Act
        var json = JsonSerializer.Serialize(definitions, _options);

        //Assert
        Console.WriteLine(json);
    }


    [JsonConverter(typeof(JsonStringEnumConverter))]
    //OR
    //[JsonConverter(typeof(JsonStringEnumConverter<IoType>))]
    //TODO: Perf Test?
    private enum IoType
    {
        [JsonStringEnumMemberName("di")]
        DigitalInput,

        [JsonStringEnumMemberName("do")]
        DigitalOutput,

        [JsonStringEnumMemberName("ai")]
        AnalogInput,

        [JsonStringEnumMemberName("ao")]
        AnalogOutput,
    }

    private class IoDefinition
    {
        public IoType Type { get; set; }
        public int Address { get; set; }
        public byte Bit { get; set; }
    }
}