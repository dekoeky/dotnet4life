using Formatters.Models;
using Formatters.Services.WeatherForecasts;
using Microsoft.AspNetCore.Mvc;

namespace Formatters.Controllers;

[ApiController]
[Route("Controllers/[controller]")]
public class WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService) : ControllerBase
{
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get() => weatherForecastService.WeatherForecasts();


    [HttpPost(Name = "PostWeatherForecast")]
    public void Post(IEnumerable<WeatherForecast> weathers)
    {
        foreach (var weatherForecast in weathers)
            logger.LogInformation("{WeatherForecast}", weatherForecast);
    }
}