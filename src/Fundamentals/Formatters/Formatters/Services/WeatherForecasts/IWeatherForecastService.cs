using Formatters.Models;

namespace Formatters.Services.WeatherForecasts;

public interface IWeatherForecastService
{
    IEnumerable<WeatherForecast> WeatherForecasts();
}