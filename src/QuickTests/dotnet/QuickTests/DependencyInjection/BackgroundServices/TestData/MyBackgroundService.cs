using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace QuickTests.DependencyInjection.BackgroundServices.TestData;

public class MyBackgroundService(int instance) : BackgroundService
{
    public const int DefaultInstance = 0;

    /// <summary>
    /// Parameterless Constructor.
    /// </summary>
    public MyBackgroundService() : this(DefaultInstance)
    {
        Debug.WriteLine("Parameterless Constructor Used!");
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        IncrementRunCount(instance);
        Console.WriteLine($"[{instance}] Executing!");
        return Task.CompletedTask;
    }


    private static readonly Dictionary<int, int> _Data = new();
    public static IReadOnlyDictionary<int, int> Data => _Data.AsReadOnly();
    public static int GetRunCount(int key) => _Data.TryGetValue(key, out var value) ? value : 0;
    public static void IncrementRunCount(int key) => _Data[key] = GetRunCount(key) + 1;
    public static void ResetRunCounts() => _Data.Clear();
}