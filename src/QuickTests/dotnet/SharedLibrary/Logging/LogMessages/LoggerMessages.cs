using Microsoft.Extensions.Logging;
using SharedLibrary.Logging.LogMessages.Models;

namespace SharedLibrary.Logging.LogMessages;

public static partial class LoggerMessages
{
    private const int EventId = 23;

    public static void CityOfResidenceStringInterpolationBadWay(this ILogger logger, string name, string city)
    {
        //This is an example of what NOT to do, because it does not produce structured logs
        //This line of code should trigger a warning for CA2254
        //https://learn.microsoft.com/dotnet/fundamentals/code-analysis/quality-rules/ca2254
        logger.LogInformation(eventId: EventId, message: $"{name} lives in {city}");
    }





    public static void CityOfResidenceSimple(this ILogger logger, Resident resident) =>
        CityOfResidenceSimple(logger, resident.Name, resident.CityOfResidence);

    public static void CityOfResidenceSimple(this ILogger logger, string name, string city) =>
        logger.LogInformation(eventId: EventId, message: "{Name} lives in {City}.", name, city);



    [LoggerMessage(LogLevel.Information, EventId = EventId, Message = "{Name} lives in {City}.")]
    public static partial void CityOfResidenceSourceGenerated(
        this ILogger logger,
        string name,
        string city);

    public static void CityOfResidenceSourceGenerated(this ILogger logger, Resident resident) =>
        CityOfResidenceSourceGenerated(logger, resident.Name, resident.CityOfResidence);


}