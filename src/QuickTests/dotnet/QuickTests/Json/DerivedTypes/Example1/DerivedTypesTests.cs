using QuickTests.Json.DerivedTypes.Example1.Models;
using System.Text.Json;

namespace QuickTests.Json.DerivedTypes.Example1;

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

    [TestMethod]
    public void SerializeBase()
    {
        //Arrange
        var data = new WeatherForecastBase
        {
            Date = new DateTimeOffset(2025, 02, 17, 16, 35, 10, DateTimeOffset.Now.Offset),
            Summary = "Good Weather",
            TemperatureCelsius = 13,
        };

        //Act
        var json = JsonSerializer.Serialize(data, _jsonSerializerOptions);

        //Assert
        Console.WriteLine(json);
    }

    [TestMethod]
    public void SerializeWithCity()
    {
        //Arrange
        var data = new WeatherForecastWithCity
        {
            Date = new DateTimeOffset(2025, 02, 17, 16, 35, 10, DateTimeOffset.Now.Offset),
            Summary = "Good Weather",
            TemperatureCelsius = 13,
            City = "Brecht"
        };

        //Act
        var json = JsonSerializer.Serialize<WeatherForecastBase>(data, _jsonSerializerOptions);

        //Assert
        Console.WriteLine(json);
    }

    [TestMethod]
    public void DeSerializeBase()
    {
        //Arrange
        const string json = """
                            {
                              "$type": "base",
                              "Date": "2025-02-17T16:35:10+01:00",
                              "TemperatureCelsius": 13,
                              "Summary": "Good Weather"
                            }
                            """;

        //Act
        var data = JsonSerializer.Deserialize<WeatherForecastBase>(json, _jsonSerializerOptions);

        //Assert
        Assert.IsNotNull(data);
        Assert.AreEqual(typeof(WeatherForecastBase), data.GetType());
    }

    [TestMethod]
    public void DeSerializeWithCity()
    {
        //Arrange
        const string json = """
                            {
                              "$type": "withCity",
                              "City": "Brecht",
                              "Date": "2025-02-17T16:35:10+01:00",
                              "TemperatureCelsius": 13,
                              "Summary": "Good Weather"
                            }
                            """;

        //Act
        var data = JsonSerializer.Deserialize<WeatherForecastBase>(json, _jsonSerializerOptions);

        //Assert
        Assert.IsNotNull(data);
        Assert.AreEqual(typeof(WeatherForecastWithCity), data.GetType());
    }
}