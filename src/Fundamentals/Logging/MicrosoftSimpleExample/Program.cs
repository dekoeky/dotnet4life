using Microsoft.Extensions.Logging;

//Logger factory defines where the logger will log to
using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
});

//Create a logger from the factory, using a class as category
ILogger logger = loggerFactory.CreateLogger<Program>();

logger.LogInformation("This is an info log.");
logger.LogError("This is an error log.");


ILogger breakfastLogger = loggerFactory.CreateLogger("breakfast");

breakfastLogger.LogDebug("Toasting bread...");
await Task.Delay(500);
breakfastLogger.LogWarning("Toast is a bit burnt, oops");
breakfastLogger.LogInformation("Toast ready!");
breakfastLogger.LogDebug("Spreading jam over toast...");
await Task.Delay(200);
breakfastLogger.LogDebug("Pouring coffee...");
await Task.Delay(300);
breakfastLogger.LogInformation("Breakfast is ready!");
breakfastLogger.LogCritical("OOPS! Dropped the plate!");