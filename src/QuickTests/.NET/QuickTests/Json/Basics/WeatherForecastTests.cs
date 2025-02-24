using QuickTests.TestModels;
using System.Text.Json;

namespace QuickTests.Json.Basics;


[TestClass]
public class WeatherForecastTests
{
    private readonly WeatherForecast _weatherForecast = new()
    {
        Date = DateOnly.FromDateTime(DateTime.Now),
        Summary = "Today was a good day",
        TemperatureC = 35,
    };

    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
    };

    private const string Json = """
                                 {
                                   "Date": "2025-01-13",
                                   "TemperatureC": 35,
                                   "TemperatureF": 94,
                                   "Summary": "Today was a good day"
                                 }
                                 """;

    [TestMethod]
    public void Serialize()
    {
        //Act
        var json = JsonSerializer.Serialize(_weatherForecast, _options);

        //Assert
        Console.WriteLine(json);
    }

    [TestMethod]
    public void DeSerialize()
    {
        //Act
        var poco = JsonSerializer.Deserialize<WeatherForecast>(Json, _options);

        //Assert
        Assert.IsNotNull(poco);
        Assert.AreEqual(35, poco.TemperatureC);
    }
}