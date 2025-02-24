using Microsoft.Extensions.Logging;

namespace QuickTests.Logging.LogMessages;

public static partial class LoggerMessages
{
    [LoggerMessage(EventId = 23, Message = "{Name} lives in {City}.")]
    public static partial void PlaceOfResidence(
        this ILogger logger,
        LogLevel logLevel,
        string name,
        string city);
}