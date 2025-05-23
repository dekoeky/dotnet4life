using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace QuickTests.Logging.LogMessages;

[TestClass]
public class LoggerMessageTests
{
    [TestMethod]
    public void UseSourceGeneratedLoggerMessage()
    {
        //Arrange:
        //Create a logger factory, that creates loggers that will log Json formatted messages to the console
        var loggerFactory = LoggerFactory.Create(ConfigureLoggingBuilder);

        //Create a logger
        var logger = loggerFactory.CreateLogger("DemoCategory");

        //Act:
        logger.PlaceOfResidence("Liana", "Seattle");
    }

    private static void ConfigureLoggingBuilder(ILoggingBuilder builder)
    {
        builder.AddJsonConsole(options =>
            options.JsonWriterOptions = new JsonWriterOptions()
            {
                Indented = true
            });
    }
}