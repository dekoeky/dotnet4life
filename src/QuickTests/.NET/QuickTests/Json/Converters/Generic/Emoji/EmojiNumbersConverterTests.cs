using QuickTests.Json.Converters.Simple;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace QuickTests.Json.Converters.Generic.Emoji;

/// <summary>
/// <see cref="PrefixedIntJsonConverterTests.PrefixedIntJsonConverter"/> related tests.
/// </summary>
[TestClass]
public class EmojiNumbersConverterTests
{
    [TestMethod]
    public void Serialize()
    {
        //Arrange
        var data = new MyData<uint> { Number = 15 };
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
        const string json = """{"Number": "1️⃣2️⃣3️⃣4️⃣5️⃣"}""";
        var jsonSerializerOptions = JsonSerializerOptions.Default;

        //Act
        var data = JsonSerializer.Deserialize<MyData<uint>>(json, jsonSerializerOptions);

        //Assert
        Console.WriteLine(json);
        Assert.IsNotNull(data);
        Assert.AreEqual(12345u, data.Number);
    }

    class MyData<T> where T : struct
    {
        [JsonConverter(typeof(EmojiNumbersConverterFactory))]
        public T Number { get; init; }
    }
}