using Serilog;
using Serilog.Filters;

namespace QuickTests.Logging.Serilog;

[TestClass]
public class SerilogDemo
{
    [TestMethod]
    public void Old()
    {
        using var logger = new LoggerConfiguration()
            //.WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] ({SourceContext}) {Message:lj}{NewLine}{Exception}")
            .MinimumLevel.Debug()
            .WriteTo.Logger(lc => lc
                //.Filter.ByIncludingOnly(Matching.FromSource("AppelSap"))
                .Filter.ByIncludingOnly(Matching.FromSource<MyMqttClient>())
                .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}"))

            // Log for a different category (e.g., "AnotherCategory") to another file
            .WriteTo.Logger(lc => lc
                //.Filter.ByIncludingOnly(Matching.FromSource("AppelSap"))
                .Filter.ByIncludingOnly(Matching.FromSource<MyHttpClient>())
                .WriteTo.Debug(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}"))

            .CreateLogger();


        logger.ForContext<MyMqttClient>().Information("My Mqtt is writing stuff");
        logger.ForContext<MyHttpClient>().Information("My Http is writing stuff");
        logger.ForContext("AppelSap").Information("My appelsap is appelsapping");
    }

    [TestMethod]
    public void foosqdq()
    {
        // Configure the logger
        using var logger = new LoggerConfiguration()
            .MinimumLevel.Debug() // Set the minimum log level
            .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}")

            // Filter and log for "AppelSap" to Debug output
            .WriteTo.Logger(lc => lc
                .Filter.ByIncludingOnly(Matching.FromSource("AppelSap"))
                .WriteTo.Debug(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}"))

            .CreateLogger();

        // Log messages with different contexts
        logger.ForContext<MyMqttClient>().Information("My Mqtt is writing stuff");
        logger.ForContext<MyHttpClient>().Information("My Http is writing stuff");
        logger.ForContext("AppelSap").Information("My appelsap is appelsapping");

    }

    private class MyMqttClient;

    private class MyHttpClient;
}
