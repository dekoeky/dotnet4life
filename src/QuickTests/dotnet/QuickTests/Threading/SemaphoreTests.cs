using System.Diagnostics;

namespace QuickTests.Threading;

[TestClass]
public class SemaphoreTests
{
    private readonly DateTime start = DateTime.Now;
    private static readonly TimeSpan delay = TimeSpan.FromSeconds(1);

    public TestContext TestContext { get; set; }

    [TestMethod]
    public async Task Demonstrate()
    {
        // Arrange
        var token = TestContext.CancellationToken;

        // Act
        WriteLine(null, "Unit test starting");
        var task1 = LockableMethod(1, token);
        var task2 = LockableMethod(2, token);

        var tasks = new[] { task1, task2 };

        await Task.WhenAll(tasks);

        // Assert
        WriteLine(null, "Unit test finished");
    }

    private readonly SemaphoreSlim _semaphore = new(1, 1);

    private async Task LockableMethod(int taskId, CancellationToken token)
    {
        WriteLine(taskId, "entered method");

        await _semaphore.WaitAsync(token);
        WriteLine(taskId, "acquired semaphore");

        try
        {

            WriteLine(taskId, "entered semaphore");

            await Task.Delay(delay, token);

            WriteLine(taskId, "will exit semaphore");
        }
        finally
        {
            //important
            _semaphore.Release();
        }

        WriteLine(taskId, "finished method");
    }

    private void WriteLine(int? task, string message)
    {
        var delta = DateTime.Now - start;
        var taskName = task is null ? "?" : task.ToString();
        Debug.WriteLine($"[{delta}] [{Environment.CurrentManagedThreadId,3}] ({taskName}) - {message}");
    }
}