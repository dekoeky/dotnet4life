using System.Text.Json;
using System.Text.Json.Serialization;

namespace QuickTests.Json.Basics.Attributes;

[TestClass]
public class JsonPropertyNameTests
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
            new() { ThisIsAVeryLongNameThatYouWouldNotTypicallyWantToSendEverytimeYouSendAJsonMessage = 10, nummertje = 0 },
            new() { ThisIsAVeryLongNameThatYouWouldNotTypicallyWantToSendEverytimeYouSendAJsonMessage = 20, nummertje = 0 },
            new() { ThisIsAVeryLongNameThatYouWouldNotTypicallyWantToSendEverytimeYouSendAJsonMessage = 10, nummertje = 1 },
            new() { ThisIsAVeryLongNameThatYouWouldNotTypicallyWantToSendEverytimeYouSendAJsonMessage = 20, nummertje = 1 },
        ];
        //Act
        var json = JsonSerializer.Serialize(definitions, _options);

        //Assert
        Console.WriteLine(json);
    }




    private class IoDefinition
    {
        public const string NameForBitAddress = "b";

        [JsonPropertyName("n")] //Possible to use string
        public int ThisIsAVeryLongNameThatYouWouldNotTypicallyWantToSendEverytimeYouSendAJsonMessage { get; set; }

        [JsonPropertyName(NameForBitAddress)] //Possible to reference const
        public byte nummertje { get; set; }
    }
}