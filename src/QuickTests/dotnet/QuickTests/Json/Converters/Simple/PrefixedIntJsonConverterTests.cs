using System.Text.Json;
using System.Text.Json.Serialization;

namespace QuickTests.Json.Converters.Simple;

/// <summary>
/// <see cref="PrefixedIntJsonConverter"/> related tests.
/// </summary>
[TestClass]
public class PrefixedIntJsonConverterTests
{
    [TestMethod]
    public void SerializeWithAttribute()
    {
        //Arrange
        var data = new DataWithAttribute { Number = 15 };
        var jsonSerializerOptions = JsonSerializerOptions.Default;

        //Act
        var json = JsonSerializer.Serialize(data, jsonSerializerOptions);

        //Assert
        Console.WriteLine(json);
    }

    [TestMethod]
    public void DeSerialize()
    {
        //Arrange
        const string json = """{"Number":"here is the number you need... 15" }""";

        //Act
        var data = JsonSerializer.Deserialize<DataWithAttribute>(json);

        //Assert
        Assert.IsNotNull(data);
        Assert.AreEqual(15, data.Number);
        Console.WriteLine(data.Number);
    }


    [TestMethod]
    public void SerializeWithConverterInOptions()
    {
        //Arrange
        var data = new DataWithoutAttribute { Number = 15 };
        var jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerOptions.Default)
        {
            Converters =
            {
                new PrefixedIntJsonConverter("ForDemoPurposesTheNumberCanBeFoundHere==>")
            }
        };

        //Act
        var json = JsonSerializer.Serialize(data, jsonSerializerOptions);

        //Assert
        Console.WriteLine(json);
    }
    [TestMethod]
    public void DeSerializeWithConverterInOptions()
    {
        //Arrange
        const string json = """{"Number":"ForDemoPurposesTheNumberCanBeFoundHere:15"}""";
        var jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerOptions.Default)
        {
            Converters =
            {
                new PrefixedIntJsonConverter("ForDemoPurposesTheNumberCanBeFoundHere:")
            }
        };

        //Act
        var data = JsonSerializer.Deserialize<DataWithoutAttribute>(json, jsonSerializerOptions);

        //Assert
        Assert.IsNotNull(data);
        Assert.AreEqual(15, data.Number);
        Console.WriteLine(data.Number);
    }

    //Has a JsonConverter defined by Attribute
    private class DataWithAttribute
    {
        [JsonConverter(typeof(PrefixedIntJsonConverter))]
        public int Number { get; init; }
    }

    private class DataWithoutAttribute
    {
        public int Number { get; init; }
    }

    /// <summary>
    /// A very basic JsonConverter that demonstrates a non-default way of serializing something simple such as an int.
    /// </summary>
    /// <param name="prefix">The prefix used for (de)serialization.</param>
    public class PrefixedIntJsonConverter(string prefix) : JsonConverter<int>
    {
        public PrefixedIntJsonConverter() : this("here is the number you need...")
        {
            //Required for using with JsonConverterAttribute
        }

        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            //Attempt to read the string value
            var s = reader.GetString() ?? throw new InvalidOperationException("Can't read string :(");

            //If all is well, this should start with our prefix
            if (!s.StartsWith(prefix)) throw new InvalidOperationException("Can't read this number :(");

            //Cut away the prefix
            s = s[prefix.Length..];

            //parse the resulting value as int
            return int.Parse(s);
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            writer.WriteStringValue($"{prefix}{value}");
        }
    }
}