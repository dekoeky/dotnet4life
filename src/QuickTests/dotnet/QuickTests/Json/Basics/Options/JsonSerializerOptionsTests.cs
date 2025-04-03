using QuickTests.TestModels;
using System.Text.Json;

namespace QuickTests.Json.Basics.Options;

[TestClass]
public class JsonSerializerOptionsTests
{
    [DataTestMethod]
    [DynamicData(nameof(OptionsToTest))]
    public void DefaultOptions(string optionsName, JsonSerializerOptions? jsonSerializerOptions)
    {
        //Arrange
        var data = new WeatherForecast
        {
            Summary = "Some weather to serialize",
            Date = DateOnly.FromDateTime(DateTime.Today),
            TemperatureC = 15,
        };

        //Act
        var json = jsonSerializerOptions is null
            ? JsonSerializer.Serialize(data)
            : JsonSerializer.Serialize(data, jsonSerializerOptions);

        //Assert
        Console.WriteLine($"Options: {optionsName}");
        Console.WriteLine(json);
    }

    public static IEnumerable<object?[]> OptionsToTest
    {
        get
        {
            yield return ["none", null];
            yield return ["JsonSerializerOptions.Default", JsonSerializerOptions.Default];
            yield return ["JsonSerializerOptions.Web", JsonSerializerOptions.Web];
            yield return ["new()", new JsonSerializerOptions()];
            yield return ["Indented", new JsonSerializerOptions
            {
                WriteIndented = true,
            }];
        }
    }
}