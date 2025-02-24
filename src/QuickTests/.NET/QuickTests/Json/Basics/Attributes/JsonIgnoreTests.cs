using System.Text.Json;

namespace QuickTests.Json.Basics.Attributes;

[TestClass]
public class JsonIgnoreTests
{
    private readonly JsonSerializerOptions _options = new()
    {
        //For readability
        WriteIndented = true,
    };

    [TestMethod]
    public void Serialize()
    {
        ////Arrange
        //var definition = new IoDefinition
        //{

        //};

        ////Act
        //var json = JsonSerializer.Serialize(definitions, _options);

        ////Assert
        //Console.WriteLine(json);
    }



    private class IoDefinition
    {

        //[JsonIgnore(Condition)] public string lksqdhjqldshdsq { get; set; } = "YO";
    }
}