using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using WebApplication1.Configuration;

namespace WebApplication1.Controllers;

[ApiController]
[Route("Controllers/[controller]")]
public class TestController(ILogger<TestController> logger) : ControllerBase
{
    [HttpGet("SingleConcurrentRequest")]
    [EnableRateLimiting(RateLimiting.SingleConcurrentRequest)]
    public async Task<string> GetSingleConcurrentRequest()
    {
        //Adding delay to easily test concurrent requests
        logger.LogInformation("Waiting 1s");
        await Task.Delay(1000);

        return "Hello World";
    }

    [HttpGet("SlidingWindow")]
    [EnableRateLimiting(RateLimiting.SlidingWindowMaxOneEveryTwoSeconds)]
    public string GetSlidingWindow() => "Hello World";
}
