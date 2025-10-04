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
    public string Name = null!;

    [Params("Compton")]
    public string City = null!;

    [ParamsAllValues]
    public bool LoggerEnabled;

    private Resident _resident = null!;
    private ILoggerFactory _loggerFactory = null!;
    private ILogger _logger = null!;

    [GlobalSetup]
    public void Setup()
    {
        _resident = new Resident
        {
            Name = Name,
            CityOfResidence = City,
        };

        //Create a logger factory, that creates loggers that will log Json formatted messages to the console
        _loggerFactory = LoggerFactory.Create(builder =>
           builder.AddProvider(new NoopProvider(LoggerEnabled)));

        //Create a logger
        _logger = _loggerFactory.CreateLogger<LoggerMessageTests>();
    }

    [GlobalCleanup]
    public void CleanUp()
    {
        _loggerFactory.Dispose();
    }

    [Benchmark]
    public void SourceGenerated_Properties()
        => _logger.CityOfResidenceSourceGenerated(Name, City);

    [Benchmark]
    public void SourceGenerated_Poco()
        => _logger.CityOfResidenceSourceGenerated(_resident);

    [Benchmark]
    public void Simple_Properties()
        => _logger.CityOfResidenceSimple(Name, City);

    [Benchmark]
    public void Simple_Poco()
        => _logger.CityOfResidenceSimple(_resident);
}