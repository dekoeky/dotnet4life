using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Formatters.Services;

public class MvcFormattersPrinterService(ILogger<MvcFormattersPrinterService> logger, IOptions<MvcOptions> options) : BackgroundService
{
    private readonly MvcOptions _options = options.Value;
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var inputFormatters = _options.InputFormatters.Select(f => f.ToString()).ToArray();
        var outputFormatters = _options.OutputFormatters.Select(f => f.ToString()).ToArray();

        logger.LogWarning($"{nameof(_options.InputFormatters)}: {{InputFormatters}}", [inputFormatters]);
        logger.LogWarning($"{nameof(_options.OutputFormatters)}: {{OutputFormatters}}", [outputFormatters]);

        return Task.CompletedTask;
    }
}