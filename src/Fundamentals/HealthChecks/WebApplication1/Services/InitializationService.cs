using WebApplication1.HealthChecks.Checks;

namespace WebApplication1.Services;

/// <summary>
/// An example service that mocks the initialization of some component.
/// </summary>
/// <seealso cref="InitializationHealthCheck"/>
/// <seealso cref="InitializationState"/>
internal class InitializationService(ILogger<InitializationService> logger, InitializationState state) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var deltaTime = TimeSpan.FromMilliseconds(500);
        var deltaPercent = 0.1;
        state.EstimatedTimeOfInitialization = DateTime.Now + (1.0 - state.PercentageInitialized) * deltaTime / deltaPercent;

        while (state.PercentageInitialized < 1.0)
        {
            logger.LogInformation("Application is being initialized: {Percentage:P}", state.PercentageInitialized);

            await Task.Delay(deltaTime, stoppingToken);
            state.PercentageInitialized += deltaPercent;
        }

        state.PercentageInitialized = 1.0;
        state.Initialized = true;
        logger.LogInformation("Application was initialized!");
    }
}