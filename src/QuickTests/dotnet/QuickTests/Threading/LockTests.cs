using System.Diagnostics;

namespace QuickTests.Threading;

//[TestClass]
public class LockTests
{
    [TestMethod]
    public async Task TestMethod1()
    {
        Debug.WriteLine($"Unit test starting on thread {Environment.CurrentManagedThreadId}");
        //Arrange
        var task1 = Task.Run(() => LockableMethod(TimeSpan.FromSeconds(2), "Task 1"), TestContext.CancellationToken);
        var task2 = Task.Run(() => LockableMethod(TimeSpan.FromSeconds(2), "Task 2"), TestContext.CancellationToken);

        var tasks = new[] { task1, task2 };

        await Task.WhenAll(tasks);
        Debug.WriteLine($"Unit test finished on thread {Environment.CurrentManagedThreadId}");
    }

    private readonly SemaphoreSlim _semaphore = new(1, 1);

    private async Task LockableMethod(TimeSpan delay, string taskName)
    {
        Debug.WriteLine($"{DateTime.Now.TimeOfDay} {taskName} entered method, on thread {Environment.CurrentManagedThreadId}");

        await _semaphore.WaitAsync();
        Debug.WriteLine($"{DateTime.Now.TimeOfDay} {taskName} acquired semaphore, on thread {Environment.CurrentManagedThreadId}");

        try
        {

            Debug.WriteLine($"{DateTime.Now.TimeOfDay} {taskName} entered semaphore, on thread {Environment.CurrentManagedThreadId}");

            await Task.Delay(delay);

            Debug.WriteLine($"{DateTime.Now.TimeOfDay} {taskName} will exit semaphore, on thread {Environment.CurrentManagedThreadId}");
        }
        finally
        {
            //important
            _semaphore.Release();
        }

        Debug.WriteLine($"{DateTime.Now.TimeOfDay} {taskName} finished method, on thread {Environment.CurrentManagedThreadId}");
    }

    public TestContext TestContext { get; set; }
}