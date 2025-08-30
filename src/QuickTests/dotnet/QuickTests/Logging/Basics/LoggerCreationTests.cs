using Microsoft.Extensions.Logging;

namespace QuickTests.Logging.Basics;

[TestClass]
public class LoggerCreationTests
{
    [TestMethod]
    public void CreateSimpleConsoleLogger()
    {
        //Create a logger factory
        //REMARK: The using statement ensures that resources are disposed of properly,
        //which is important to ensure all logs are flushed before the application exits.
        using var loggerFactory = LoggerFactory.Create(configure => configure
            .SetMinimumLevel(LogLevel.Debug)
            .AddConsole());

        //Create a logger, with our test class as the category name
        var logger = loggerFactory.CreateLogger<LoggerCreationTests>();

        //Log a simple message, for demonstration
        logger.LogInformation("Hello, World!");
    }

    [TestMethod]
    public void LoggerCategories()
    {
        //Create a logger factory
        //REMARK: The using statement ensures that resources are disposed of properly,
        //which is important to ensure all logs are flushed before the application exits.
        using var loggerFactory = LoggerFactory.Create(configure => configure
            .SetMinimumLevel(LogLevel.Debug)
            .AddConsole());

        //Create loggers with different category names
        var loggerService1 = loggerFactory.CreateLogger<myService1>();
        var loggerService2 = loggerFactory.CreateLogger<myService2>();
        var loggerDbContext = loggerFactory.CreateLogger<myDbContext>();
        var loggerCustom1 = loggerFactory.CreateLogger("Custom1");
        var loggerCustom2 = loggerFactory.CreateLogger("Custom2");

        //Log (different) messages from each logger to demonstrate category names
        loggerService1.LogInformation("Hello from Service1");
        loggerService2.LogInformation("Hello from Service2");
        loggerDbContext.LogInformation("Hello from DbContext");
        loggerCustom1.LogInformation("Hello from Custom1");
        loggerCustom2.LogInformation("Hello from Custom2");
    }

    private class myService1;
    private class myService2;
    private class myDbContext;
}