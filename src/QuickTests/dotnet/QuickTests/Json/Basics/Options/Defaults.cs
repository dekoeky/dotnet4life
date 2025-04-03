using QuickTests.TestModels;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace QuickTests.Json.Basics.Options;

[TestClass]
public class Defaults
{
    [DataTestMethod]
    [DataRow(JsonSerializerDefaults.General)]
    [DataRow(JsonSerializerDefaults.Web)]
    [DataRow(null)]
    public void PrintJsonSerializerDefaults(JsonSerializerDefaults? defaults)
    {
        //Arrange
        var options = defaults is null
            ? new JsonSerializerOptions()
            : new JsonSerializerOptions(defaults.Value);

        //Act
        var dataToPrint = new
        {
            options.WriteIndented,
            options.AllowTrailingCommas,
            Converters = options.Converters.Select(c => c.Type?.Name).ToArray(),
            options.DefaultIgnoreCondition,
            options.IncludeFields,
            options.PropertyNamingPolicy,
        };
        var json = JsonSerializer.Serialize(dataToPrint);

        //Assert
        Console.WriteLine(json);
    }

    [TestMethod]
    public void TryGetTypeInfo()
    {
        JsonSerializerOptions[] optionsToBeTested =
        [
            JsonSerializerOptions.Default,
            new(),
            new(JsonSerializerOptions.Default),
            new() { TypeInfoResolver = new DefaultJsonTypeInfoResolver() }
        ];
        var t = typeof(WeatherForecast);

        foreach (var options in optionsToBeTested)
            if (options.TryGetTypeInfo(t, out _))
                Console.WriteLine($"successfully retrieved TypeInfo for {t}");
            else
                Console.WriteLine($"failed to retrieve TypeInfo for {t}");
    }
}