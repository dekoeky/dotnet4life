using System.Net.Http.Json;

namespace ClassLibrary1.Clients;

public class TimeApiClient(HttpClient client) : ITimeApiClient
{
    public async Task<TimeSpan> GetTimeOfDay(CancellationToken cancellationToken = default)
        => await client.GetFromJsonAsync<TimeSpan>("TimeOfDay", cancellationToken);

    public async Task<DateOnly> GetDate(CancellationToken cancellationToken = default)
        => await client.GetFromJsonAsync<DateOnly>("Date", cancellationToken);

    public async Task<DateTime> GetTimestamp(CancellationToken cancellationToken = default)
        => await client.GetFromJsonAsync<DateTime>("Timestamp", cancellationToken);
}