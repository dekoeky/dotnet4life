using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Shared.Services;

public class Worker1(ILogger<Worker1> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogRandomLevel("Worker1 running at: {time}", DateTimeOffset.Now);

            await Task.Delay(1000, stoppingToken);
        }
    }
}