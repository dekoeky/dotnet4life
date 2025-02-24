// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Logging;

var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
});

ILogger logger = loggerFactory.CreateLogger<Program>();

logger.LogInformation("This is an info log.");
logger.LogError("This is an error log.");
