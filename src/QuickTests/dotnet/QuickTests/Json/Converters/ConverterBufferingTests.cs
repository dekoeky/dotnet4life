using System.Text.Json;
using System.Text.Json.Serialization;

namespace QuickTests.Json.Converters;

[TestClass]
public class ConverterBufferingTests
{
    [TestMethod]
    public void ConverterIsBuffered()
    {
        //Arrange
        var data1 = new MyPoco();
        var data2 = new MyPoco();
        var jsonSerializerOptions = JsonSerializerOptions.Default;

        //Act
        _ = JsonSerializer.Serialize(data1, jsonSerializerOptions);
        _ = JsonSerializer.Serialize(data2, jsonSerializerOptions);

        //Assert
        Assert.AreEqual(1, MyConverter.CtorCount);
    }
}
file class MyPoco
{
    public int NormalInt { get; set; } = 1;

    [JsonConverter(typeof(MyConverter))]
    public int SpecialInt { get; set; } = 2;
}
file class MyConverter : JsonConverter<int>
{
    private static int _count;
    public static int CtorCount => _count;

    public MyConverter()
    {
        Interlocked.Increment(ref _count);
    }
    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<int>(ref reader, options);
    }

    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
    }
}