using Microsoft.Extensions.Logging;
using SharedLibrary.Logging.LogMessages.Models;
using System.Runtime.CompilerServices;

namespace QuickTests.Logging.LogMessages;

public static partial class LoggerMessages
{
    [LoggerMessage(LogLevel.Information, EventId = 23, Message = "{Name} lives in {City}.")]
    public static partial void CityOfResidence(
        this ILogger logger,
        string name,
        string city);

    public static void CityOfResidence(this ILogger logger, Resident resident)
        => CityOfResidence(logger, resident.Name, resident.CityOfResidence);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CityOfResidenceAggressiveInlining(this ILogger logger, Resident resident)
        => CityOfResidence(logger, resident.Name, resident.CityOfResidence);

    public static void CityOfResidenceSimple(
      this ILogger logger,
      string name,
      string city) => logger.LogInformation(eventId: 23, message: "{Name} lives in {City}.", name, city);

    public static void CityOfResidenceSimple(this ILogger logger, Resident resident)
    => CityOfResidenceSimple(logger, resident.Name, resident.CityOfResidence);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CityOfResidenceSimpleAggressiveInlining(this ILogger logger, Resident resident)
        => CityOfResidenceSimple(logger, resident.Name, resident.CityOfResidence);
}
