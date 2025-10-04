using Serilog;

namespace QuickTests.Logging.Serilog;

internal static class SerilogExtensions
{
    public static ILogger ForContext(this ILogger logger, string sourceContextPropertyName)
        => logger.ForContext(global::Serilog.Core.Constants.SourceContextPropertyName, sourceContextPropertyName);
}