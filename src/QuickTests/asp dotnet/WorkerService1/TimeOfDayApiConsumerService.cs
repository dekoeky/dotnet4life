using ClassLibrary1.Clients;

namespace WorkerService1;

public class TimeOfDayApiConsumerService(ILogger<TimeOfDayApiConsumerService> logger, ITimeApiClient client) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var time = await client.GetTimeOfDay(stoppingToken);
                logger.LogInformation("Time Retrieved From Api: {time}", time);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error");
            }
            finally
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}