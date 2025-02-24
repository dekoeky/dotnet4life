using Microsoft.Extensions.Logging;

namespace Shared;

public static class MicrosoftIloggerExtensions
{
    private static readonly LogLevel[] Levels = Enum
        .GetValues<LogLevel>().Except([LogLevel.None])
        .ToArray();

    public static void LogRandomLevel(this ILogger logger, string? message, params object?[] args)
    {
        var randomLevel = Levels[Random.Shared.Next(Levels.Length)];

#pragma warning disable CA2254
        logger.Log(randomLevel, message, args);
#pragma warning restore CA2254
    }
}