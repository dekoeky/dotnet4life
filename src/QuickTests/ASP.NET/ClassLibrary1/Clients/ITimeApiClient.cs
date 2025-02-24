namespace ClassLibrary1.Clients;

public interface ITimeApiClient
{
    Task<TimeSpan> GetTimeOfDay(CancellationToken cancellationToken = default);
}