using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Logging;
using SharedLibrary.Logging;
using SharedLibrary.Logging.LogMessages;
using SharedLibrary.Logging.LogMessages.Models;

namespace PerformanceTests.Benchmarks.Logging.LogMessages;

[ShortRunJob]
[MemoryDiagnoser]
public class LoggerMessageTests
{
    [Params("Kendrick")]
    public string Name;

    [Params("Compton")]
    public string City;

    [ParamsAllValues]
    public bool LoggerEnabled;

    private Resident resident;
    private ILoggerFactory loggerFactory;
    private ILogger logger;

    [GlobalSetup]
    public void Setup()
    {
        resident = new Resident
        {
            Name = Name,
            CityOfResidence = City,
        };

        //Create a logger factory, that creates loggers that will log Json formatted messages to the console
        loggerFactory = LoggerFactory.Create(builder =>
           builder.AddProvider(new NoopProvider(LoggerEnabled)));

        //Create a logger
        logger = loggerFactory.CreateLogger<LoggerMessageTests>();
    }

    [GlobalCleanup]
    public void CleanUp()
    {
        loggerFactory.Dispose();
    }

    [Benchmark]
    public void SourceGenerated_Properties()
        => logger.CityOfResidenceSourceGenerated(Name, City);

    [Benchmark]
    public void SourceGenerated_Poco()
        => logger.CityOfResidenceSourceGenerated(resident);

    [Benchmark]
    public void Simple_Properties()
        => logger.CityOfResidenceSimple(Name, City);

    [Benchmark]
    public void Simple_Poco()
        => logger.CityOfResidenceSimple(resident);
}