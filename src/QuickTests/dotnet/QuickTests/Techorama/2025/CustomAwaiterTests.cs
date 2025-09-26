using SharedLibrary.Techorama._2025;
using System.Diagnostics;

namespace QuickTests.Techorama._2025;

/// <summary>
/// <see cref="CustomStringAwaiter"/> related tests.
/// </summary>
/// <seealso cref="CustomStringAwaiterExtensions"/>
/// <seealso cref="CustomStringAwaiter"/>
[TestClass]
public class CustomAwaiterTests
{
    [TestMethod]
    [DataRow("Hello World!")]
    public async Task Await_A_String(string myString)
    {
        //ARRANGE
        const double tolerance = 0.1d; //10%
        const int delayPerLetter = 100;
        var expectedMs = myString.Length * delayPerLetter;
        var toleranceMs = expectedMs * tolerance;
        var toleranceLow = expectedMs - toleranceMs;
        var toleranceHigh = expectedMs + toleranceMs;

        //ACT
        var start = Stopwatch.GetTimestamp();
        await myString;
        var tookMilliseconds = Stopwatch.GetElapsedTime(start).TotalMilliseconds;


        //ASSERT
        Console.WriteLine($"Input:     {myString} ({myString.Length} characters)");
        Console.WriteLine($"Took:      {tookMilliseconds:0}");
        Console.WriteLine($"Expected:  {expectedMs:0}");
        Console.WriteLine($"Tolerance: {tolerance:P} / {toleranceMs:0}ms ({toleranceLow:0}ms - {toleranceHigh:0}ms)");
        Assert.AreEqual(expectedMs, tookMilliseconds, toleranceMs);
    }
}