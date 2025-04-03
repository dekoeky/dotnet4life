using WebApplication1.Models;

namespace WebApplication1.Services;

public interface IWeatherForecastService
{
    IEnumerable<WeatherForecast> WeatherForecasts();
}