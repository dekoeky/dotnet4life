using System.Text.Json;

namespace QuickTests.Json.TypeSpecific;

[TestClass]
public class DateTimeTests
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        WriteIndented = true,
    };

    [DataTestMethod]
    [DataRow(typeof(DateTime))]
    [DataRow(typeof(byte[]))]
    public void GetConverter(Type type)
    {
        //Act
        var converter = JsonSerializerOptions.GetConverter(type);
        var converterType = converter.GetType();

        //Assert
        Console.WriteLine($"{type.Name} Converter: {converterType}");
        //Assert.IsTrue(converter is JsonConverter<DateTime>);
        //Internal type:
        //System.Text.Json.Serialization.Converters.DateTimeConverter
    }

    [DataTestMethod]
    [DataRow("2024-12-10T15:00:00", DateTimeKind.Unspecified)]
    [DataRow("2024-12-10T14:00:00Z", DateTimeKind.Utc)]
    [DataRow("2024-12-10T15:00:00+01:00", DateTimeKind.Local)]
    public void GetDateTime(string dateTimeString, DateTimeKind expected)
    {
        //Arrange
        var json = $"\"{dateTimeString}\""; //DateTimes must be un quotes in json!

        //Act
        var result = JsonSerializer.Deserialize<DateTime>(json, JsonSerializerOptions);


        //Assert
        Assert.AreEqual(expected, result.Kind);
        Assert.IsTrue(result > new DateTime(2024, 12, 10));
        Assert.IsTrue(result < new DateTime(2024, 12, 11));
    }

    [DataTestMethod]
    [DataRow("2024-12-10T15:00:00", DateTimeKind.Unspecified)]
    [DataRow("2024-12-10T14:00:00Z", DateTimeKind.Utc)]
    [DataRow("2024-12-10T15:00:00+01:00", DateTimeKind.Local)]
    public void GetDateTimeProperty(string dateTimeString, DateTimeKind expected)
    {
        //Arrange
        const string propertyName = "datetime";


        var jsonProperty = $$"""
                             {
                                "{{propertyName}}": "{{dateTimeString}}"
                             }
                             """;

        //Act
        var jsonDoc = JsonSerializer.Deserialize<JsonDocument>(jsonProperty, JsonSerializerOptions) ?? throw new InvalidOperationException();
        var dateTimeProperty = jsonDoc.RootElement.GetProperty(propertyName);
        var result = dateTimeProperty.GetDateTime();

        //Assert
        Assert.IsTrue(result > new DateTime(2024, 12, 10));
        Assert.IsTrue(result < new DateTime(2024, 12, 11));
        Assert.AreEqual(expected, result.Kind);
    }


    [TestMethod]
    public void DefaultConverter()
    {
        //ARRANGE
        var options = JsonSerializerOptions.Default;

        //ACT
        var converter = options.GetConverter(typeof(DateTime));
        var type = converter.GetType();

        //ASSERT
        Assert.AreEqual("System.Text.Json.Serialization.Converters.DateTimeConverter", type.FullName);
    }
}