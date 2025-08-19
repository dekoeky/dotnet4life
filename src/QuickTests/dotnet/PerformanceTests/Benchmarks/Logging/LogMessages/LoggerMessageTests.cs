using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Logging;
using QuickTests.Logging.LogMessages;
using SharedLibrary.Logging.LogMessages.Models;
using System.Text.Json;

namespace PerformanceTests.Benchmarks.Logging.LogMessages;

[MemoryDiagnoser]
public class LoggerMessageTests
{
    [Params("Kendrick")]
    public string Name;

    [Params("Compton")]
    public string City;

    private Resident resident;
    private ILogger logger;

    [GlobalSetup]
    public void Setup()
    {
        resident = new Resident
        {
            Name = Name,
            CityOfResidence = City,
        };

        logger = CreateLogger();
    }

    private static ILogger CreateLogger()
    {
        //Create a logger factory, that creates loggers that will log Json formatted messages to the console
        var loggerFactory = LoggerFactory.Create(ConfigureLoggingBuilder);

        //Create a logger
        return loggerFactory.CreateLogger("DemoCategory");
    }

    private static void ConfigureLoggingBuilder(ILoggingBuilder builder)
    {
        builder.AddJsonConsole(options =>
            options.JsonWriterOptions = new JsonWriterOptions()
            {
                Indented = true
            });
    }

    [Benchmark]
    public void SourceGenerated_Properties()
        => logger.CityOfResidence(Name, City);

    [Benchmark]
    public void SourceGenerated_Poco()
        => logger.CityOfResidence(resident);

    [Benchmark]
    public void SourceGenerated_Poco_AggressiveInlining()
        => logger.CityOfResidenceAggressiveInlining(resident);


    [Benchmark]
    public void Simple_Properties()
        => logger.CityOfResidenceSimple(Name, City);

    [Benchmark]
    public void Simple_Poco()
        => logger.CityOfResidenceSimple(resident);

    [Benchmark]
    public void Simple_Poco_AggressiveInlining()
        => logger.CityOfResidenceSimpleAggressiveInlining(resident);
}