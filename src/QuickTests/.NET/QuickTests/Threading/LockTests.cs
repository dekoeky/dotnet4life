using System.Diagnostics;
using static System.Threading.Thread;

namespace QuickTests.Threading;

//[TestClass]
public class LockTests
{
    [TestMethod]
    public async Task TestMethod1()
    {
        Debug.WriteLine($"Unit test starting on thread {CurrentThread.ManagedThreadId}");
        //Arrange
        var task1 = Task.Run(() => LockableMethod(TimeSpan.FromSeconds(2), "Task 1"));
        var task2 = Task.Run(() => LockableMethod(TimeSpan.FromSeconds(2), "Task 2"));

        var tasks = new[] { task1, task2 };

        await Task.WhenAll(tasks);
        Debug.WriteLine($"Unit test finished on thread {CurrentThread.ManagedThreadId}");
    }

    private readonly SemaphoreSlim _semaphore = new(1, 1);

    private async Task LockableMethod(TimeSpan delay, string taskName)
    {
        Debug.WriteLine($"{DateTime.Now.TimeOfDay} {taskName} entered method, on thread {CurrentThread.ManagedThreadId}");

        await _semaphore.WaitAsync();
        Debug.WriteLine($"{DateTime.Now.TimeOfDay} {taskName} acquired semaphore, on thread {CurrentThread.ManagedThreadId}");

        try
        {

            Debug.WriteLine($"{DateTime.Now.TimeOfDay} {taskName} entered semaphore, on thread {CurrentThread.ManagedThreadId}");

            await Task.Delay(delay);

            Debug.WriteLine($"{DateTime.Now.TimeOfDay} {taskName} will exit semaphore, on thread {CurrentThread.ManagedThreadId}");
        }
        finally
        {
            //important
            _semaphore.Release();
        }

        Debug.WriteLine($"{DateTime.Now.TimeOfDay} {taskName} finished method, on thread {CurrentThread.ManagedThreadId}");
    }
}