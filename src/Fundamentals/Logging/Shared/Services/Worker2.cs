using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Shared.Services;


public class Worker2(ILogger<Appelsap> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogRandomLevel("Worker2 running at: {time}", DateTimeOffset.Now);

            await Task.Delay(3000, stoppingToken);
        }
    }
}