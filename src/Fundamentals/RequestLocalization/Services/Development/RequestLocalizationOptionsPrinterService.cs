using Microsoft.Extensions.Options;

namespace RequestLocalization.Services.Development;

public class RequestLocalizationOptionsPrinterService(ILogger<RequestLocalizationOptionsPrinterService> logger, IOptions<RequestLocalizationOptions> options) : BackgroundService
{
    private readonly RequestLocalizationOptions _options = options.Value;
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var requestCultureProviders = _options.RequestCultureProviders.Select(p => p.ToString()).ToArray();
        var supportedCultures = _options.SupportedCultures?.Select(p => p.ToString()).ToArray();
        var supportedUiCultures = _options.SupportedUICultures?.Select(p => p.ToString()).ToArray();

        logger.LogWarning($"{nameof(_options.RequestCultureProviders)}: {{RequestCultureProviders}}", [requestCultureProviders]);
        logger.LogWarning($"{nameof(_options.SupportedCultures)}: {{SupportedCultures}}", [supportedCultures]);
        logger.LogWarning($"{nameof(_options.SupportedUICultures)}: {{SupportedUICultures}}", [supportedUiCultures]);

        return Task.CompletedTask;
    }
}