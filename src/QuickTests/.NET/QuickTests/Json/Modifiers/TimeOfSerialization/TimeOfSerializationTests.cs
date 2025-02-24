using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace QuickTests.Json.Modifiers.TimeOfSerialization;

[TestClass]
public class TimeOfSerializationTests
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        TypeInfoResolver = new DefaultJsonTypeInfoResolver
        {
            Modifiers =
            {
                MyModifiers.AddSerializationTime,
            }
        }
    };


    [TestMethod]
    public void SerializationTime()
    {
        //Arrange

        var data = new SomePoco
        {
            MyTime = new DateTime(2024, 01, 01, 00, 00, 00),
            MyName = "Test",
        };

        //Act
        var json = JsonSerializer.Serialize(data, Options);

        //Assert
        Console.WriteLine(json);
        Assert.IsTrue(json.Contains("""
                                    "$TimeOfSerialization":
                                    """), "The resulting Json does not contain the expected Time Of Serialization");
    }
}