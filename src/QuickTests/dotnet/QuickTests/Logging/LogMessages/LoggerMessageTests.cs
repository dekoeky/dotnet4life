using Microsoft.Extensions.Logging;
using SharedLibrary.Logging.LogMessages;
using SharedLibrary.Logging.LogMessages.Models;
using System.Text.Json;

namespace QuickTests.Logging.LogMessages;

[TestClass]
public class LoggerMessageTests : IDisposable
{
    private readonly ILogger _logger;
    private readonly ILoggerFactory _loggerFactory;

    public LoggerMessageTests()
    {
        //Write to json console, so we can see the structured log output
        _loggerFactory = LoggerFactory.Create(builder =>
            builder.AddJsonConsole(options =>
                options.JsonWriterOptions = new JsonWriterOptions
                {
                    Indented = true, //Make it easier to read in the test output
                }));

        //Create a logger
        _logger = _loggerFactory.CreateLogger<LoggerMessageTests>();
    }

    private static readonly Resident Resident = new()
    {
        Name = "Liana",
        CityOfResidence = "Seattle",
    };

    [TestMethod]
    public void CityOfResidenceSimple()
    {
        //Act:
        _logger.CityOfResidenceSimple(Resident.Name, Resident.CityOfResidence);
    }

    [TestMethod]
    public void CityOfResidenceSourceGenerated()
    {
        //Act:
        _logger.CityOfResidenceSourceGenerated(Resident.Name, Resident.CityOfResidence);
    }


    [TestMethod]
    public void CityOfResidenceStringInterpolationBadWay()
    {
        //Act:
        _logger.CityOfResidenceStringInterpolationBadWay(Resident.Name, Resident.CityOfResidence);
    }

    void IDisposable.Dispose() => _loggerFactory.Dispose(); //To make sure messages are flushed
}