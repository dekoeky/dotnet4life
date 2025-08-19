using Microsoft.Extensions.Logging;
using SharedLibrary.Logging.LogMessages.Models;
using System.Text.Json;

namespace QuickTests.Logging.LogMessages;

[TestClass]
public class LoggerMessageTests : IDisposable
{
    private readonly ILogger logger;
    private readonly ILoggerFactory loggerFactory;

    public LoggerMessageTests()
    {
        //Create a logger factory, that creates loggers that will log Json formatted messages to the console
        loggerFactory = LoggerFactory.Create(ConfigureLoggingBuilder);

        //Create a logger
        logger = loggerFactory.CreateLogger("DemoCategory");
    }

    private static void ConfigureLoggingBuilder(ILoggingBuilder builder)
    {
        builder.AddJsonConsole(options =>
            options.JsonWriterOptions = new JsonWriterOptions()
            {
                Indented = true,
            });
    }

    private static readonly Resident resident = new()
    {
        Name = "Liana",
        CityOfResidence = "Seattle",
    };

    [TestMethod]
    public void CityOfResidence()
    {
        //Act:
        logger.CityOfResidence(resident.Name, resident.CityOfResidence);
    }

    [TestMethod]
    public void CityOfResidenceSimple()
    {
        //Act:
        logger.CityOfResidenceSimple(resident.Name, resident.CityOfResidence);
    }

    [TestMethod]
    public void CityOfResidenceSimpleAggressiveInlining()
    {
        //Act:
        logger.CityOfResidenceSimpleAggressiveInlining(resident);
    }

    [TestMethod]
    public void CityOfResidenceAggressiveInlining()
    {
        //Act:
        logger.CityOfResidenceAggressiveInlining(resident);
    }

    void IDisposable.Dispose() => loggerFactory.Dispose(); //To make sure messages are flushed
}