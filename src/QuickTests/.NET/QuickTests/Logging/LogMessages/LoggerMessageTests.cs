using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace QuickTests.Logging.LogMessages;

[TestClass]
public class LoggerMessageTests
{
    [TestMethod]
    public void UseSourceGeneratedLoggerMessage()
    {
        //Arrange:
        //Create a logger factory, that creates loggers that will log Json formatted messages to the console
        var loggerFactory = LoggerFactory.Create(builder =>
            builder.AddJsonConsole(options =>
                options.JsonWriterOptions = new JsonWriterOptions()
                {
                    Indented = true
                }));


        //Create a logger
        var logger = loggerFactory.CreateLogger("DemoCategory");

        //Act:
        logger.PlaceOfResidence(logLevel: LogLevel.Information, name: "Liana", city: "Seattle");
    }
}