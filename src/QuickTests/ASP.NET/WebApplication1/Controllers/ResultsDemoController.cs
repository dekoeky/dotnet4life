using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

/// <summary>
/// Controller to demonstrate different results
/// </summary>
/// <param name="weatherForecastService"></param>
/// <param name="logger"></param>
[ApiController]
[Route("Controllers/[controller]")]
public class ResultsDemoController(ILogger<ResultsDemoController> logger) : ControllerBase
{


    private readonly ILogger<ResultsDemoController> _logger = logger;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WeatherForecast))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Get(int i) => i switch
    {
        1 => Ok(new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Today),
            Summary = "Damn Warm",
            TemperatureC = 32,
        }),
        2 => NotFound("Who?"),
        3 => BadRequest("What?"),
        4 => Problem("oooooops, this is not good"),

        _ => throw new NotImplementedException("This was not foreseen")
    };
}