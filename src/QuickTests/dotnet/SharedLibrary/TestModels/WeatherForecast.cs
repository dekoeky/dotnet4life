namespace SharedLibrary.TestModels;

public class WeatherForecast
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }

    /// <summary>
    /// A default sample WeatherForecast instance for simple tests.
    /// </summary>

    public static readonly WeatherForecast Sample = new()
    {
        Summary = "Some sample weather",
        Date = new DateOnly(2025, 02, 24),
        TemperatureC = 24,
    };
}