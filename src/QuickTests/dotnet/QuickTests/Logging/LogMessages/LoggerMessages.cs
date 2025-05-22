using Microsoft.Extensions.Logging;

namespace QuickTests.Logging.LogMessages;

public static partial class LoggerMessages
{
    [LoggerMessage(LogLevel.Information, EventId = 23, Message = "{Name} lives in {City}.")]
    public static partial void PlaceOfResidence(
        this ILogger logger,
        string name,
        string city);
}