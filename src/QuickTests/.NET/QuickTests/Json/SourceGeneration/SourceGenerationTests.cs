using QuickTests.TestModels;
using System.Text.Json;

namespace QuickTests.Json.SourceGeneration;

[TestClass]
public class SourceGenerationTests
{
    [TestMethod]
    public void TestMethod1()
    {
        //Arrange
        var weather = WeatherForecast.Sample;

        //Act
        var json = JsonSerializer.Serialize(weather, SourceGenerationContext.Default.WeatherForecast);

        //Assert
        Console.WriteLine(json);
    }
}