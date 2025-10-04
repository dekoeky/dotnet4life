using System.Text.Json.Serialization;

namespace QuickTests.Json.DerivedTypes.Example1.Models;

[JsonDerivedType(typeof(WeatherForecastBase), typeDiscriminator: "base")]
[JsonDerivedType(typeof(WeatherForecastWithCity), typeDiscriminator: "withCity")]
public class WeatherForecastBase
{
    public DateTimeOffset Date { get; init; }
    public int TemperatureCelsius { get; init; }
    public string? Summary { get; init; }
}