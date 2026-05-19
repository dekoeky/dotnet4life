using WebApplication1.Models;

namespace WebApplication1.Services.Weather;

public interface IWeatherForecastService
{
    IEnumerable<WeatherForecast> WeatherForecasts();
}