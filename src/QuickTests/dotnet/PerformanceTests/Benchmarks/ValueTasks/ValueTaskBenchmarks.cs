using BenchmarkDotNet.Attributes;
using System.Diagnostics;

namespace PerformanceTests.Benchmarks.ValueTasks;

/// <summary>
/// Benchmark that demonstrates the performance benefits (mainly due to allocations and GC) of 
/// using <see cref="ValueTask{TResult}"/> over <see cref="Task{TResult}"/>.
/// </summary>
/// <seealso cref="https://www.youtube.com/shorts/g5Y1LwF5_nk">Idea from Nick Chapsas.</seealso>
[MemoryDiagnoser]
public class ValueTaskBenchmarks
{
    private readonly Dictionary<string, string> _cache = new()
    {
        ["key1"] = "Cached Value for Key 1"
    };

    async Task<string> GetFromCacheWithTask(string key)
    {
        if (_cache.TryGetValue(key, out var value))
        {
            Debug.WriteLine("  Cache hit!  ");
            return value;
        }

        Debug.WriteLine("  Cache miss - retrieving value from database");
        await Task.Delay(100); // Simulate DB call
        return "value from db";
    }

    async ValueTask<string> GetFromCacheWithValueTask(string key)
    {
        if (_cache.TryGetValue(key, out var value))
        {
            Debug.WriteLine("  Cache hit!  ");

            // Since we are using ValueTask, this branch does not need to allocate any awaiter or task --> Major performance/allocation benefit!
            return value;
        }

        Debug.WriteLine("  Cache miss - retrieving value from database");
        await Task.Delay(100); // Simulate DB call
        return "value from db";
    }

    [Benchmark(Description = "Task<T> (cache hit)")]
    public async Task<string> Task_CacheHitAsync() =>
        await GetFromCacheWithTask("key1");

    [Benchmark(Description = "ValueTask<T> (cache hit)")]
    public async ValueTask<string> ValueTask_CacheHitAsync() =>
        await GetFromCacheWithValueTask("key1");


    [Benchmark(Description = "ValueTask<T> (cache miss)")]
    public async ValueTask<string> ValueTask_CacheMissAsync() =>
        await GetFromCacheWithValueTask("key2");

    [Benchmark(Description = "Task<T> (cache miss)")]
    public async Task<string> Task_CacheMissAsync() =>
        await GetFromCacheWithTask("key2");



    //| Method                      | Mean              | Error          | StdDev         | Gen0   | Allocated |
    //|---------------------------- |------------------:|---------------:|---------------:|-------:|----------:|
    //| 'ValueTask<T> (cache hit)'  |          29.47 ns |       0.052 ns |       0.044 ns |      - |         - |
    //| 'ValueTask<T> (cache miss)' | 108,516,368.57 ns | 332,470.129 ns | 294,726.109 ns |      - |     416 B |
    //| 'Task<T> (cache hit)'       |          30.63 ns |       0.144 ns |       0.128 ns | 0.0138 |     144 B |
    //| 'Task<T> (cache miss)'      | 108,115,217.33 ns | 493,891.267 ns | 461,986.198 ns |      - |     448 B |

}
