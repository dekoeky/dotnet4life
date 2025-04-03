using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("Controllers/[controller]")]
public class WeatherForecastController(IWeatherForecastService weatherForecastService, ILogger<WeatherForecastController> logger) : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger = logger;

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get() => weatherForecastService.WeatherForecasts();
}