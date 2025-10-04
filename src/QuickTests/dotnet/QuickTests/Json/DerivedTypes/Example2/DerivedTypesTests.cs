using QuickTests.Json.DerivedTypes.Example2.Models;
using System.Text.Json;

namespace QuickTests.Json.DerivedTypes.Example2;

/// <summary>
/// <see href="https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/polymorphism"/>
/// </summary>
[TestClass]
public class DerivedTypesTests
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        WriteIndented = true,
    };

    private static IEnumerable<object[]> SerializeTestData()
    {
        yield return [new SetBitControlMessage { Bit = 32, Value = true, }];
        yield return [new SetFloatControlMessage { Name = "WantedTemperature", Value = 21.0f, }];
        yield return [new RestartControlMessage()];
    }

    [TestMethod]
    [DynamicData(nameof(SerializeTestData))]
    public void Serialize(IControlMessage controlMessage)
    {
        //Act
        var json = JsonSerializer.Serialize(controlMessage, _jsonSerializerOptions);

        //Assert
        Console.WriteLine(json);
    }

    [TestMethod]
    public void DeSerializeRestartControlMessage()
    {
        //Arrange
        const string json =
            """
            {
              "$cmd": "restart"
            }
            """;

        //Act
        var data = JsonSerializer.Deserialize<IControlMessage>(json, _jsonSerializerOptions);

        //Assert
        Assert.IsTrue(data is RestartControlMessage);
    }

    [TestMethod]
    public void DeSerializeSetFloatControlMessage()
    {
        //Arrange
        const string json =
            """
            {
              "$cmd": "setFloat",
              "Name": "WantedTemperature",
              "Value": 21
            }
            """;

        //Act
        var data = JsonSerializer.Deserialize<IControlMessage>(json, _jsonSerializerOptions);

        //Assert
        if (data is not SetFloatControlMessage setFloat)
        {
            Assert.Fail("Invalid Type");
        }
        else
        {
            Assert.AreEqual("WantedTemperature", setFloat.Name);
            Assert.AreEqual(21.0f, setFloat.Value);
        }
    }

    [TestMethod]
    public void DeSerializeSetBitControlMessage()
    {
        //Arrange
        const string json =
            """
            {
              "$cmd": "setBit",
              "Bit": 32,
              "Value": true
            }
            """;

        //Act
        var data = JsonSerializer.Deserialize<IControlMessage>(json, _jsonSerializerOptions);

        //Assert
        if (data is not SetBitControlMessage setBit)
        {
            Assert.Fail("Invalid Type");
        }
        else
        {
            Assert.AreEqual(32, setBit.Bit);
            Assert.AreEqual(true, setBit.Value);
        }
    }

    [TestMethod]
    public void DeSerializeRestartControlMessageAdmin()
    {
        //Arrange
        const string json =
            """
            {
              "$cmd": "restart"
            }
            """;

        //Act
        var data = JsonSerializer.Deserialize<IAdminControlMessage>(json, _jsonSerializerOptions);

        //Assert
        Assert.IsTrue(data is RestartControlMessage);
    }

    [TestMethod]
    public void SerializeRestartControlMessageAdmin()
    {
        //Arrange
        var data = new RestartControlMessage();

        //Act
        var json = JsonSerializer.Serialize<IAdminControlMessage>(data, _jsonSerializerOptions);

        //Assert
        Console.WriteLine(json);
    }


    //IEnumerable<(string Json, IControlMessage ControlMessage)>
    //{
    //    yield return ();
    //}
}