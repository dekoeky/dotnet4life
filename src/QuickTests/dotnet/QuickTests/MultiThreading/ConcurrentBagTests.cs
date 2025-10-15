using System.Collections.Concurrent;

namespace QuickTests.MultiThreading;

[TestClass]
public class ConcurrentBagTests
{
    private readonly int producers = Environment.ProcessorCount * 4;     // Make sure there are enough producers simultaneously trying to access the list to make the problem more likely.
    private const int PerProducer = 100_000;

    [TestMethod]
    public void List_is_not_thread_safe_for_writes()
    {
        //ARRANGE
        var bag = new List<int>();

        var droppedDueToException = 0;

        //ACT
        Parallel.For(0, producers, _ =>
        {
            for (var i = 0; i < PerProducer; i++)
                try
                {
                    bag.Add(i); // data race
                }
                catch (ArgumentException)
                {
                    // System.ArgumentException: 'Source array was not long enough. Check the source index, length, and the array's lower bounds. (Parameter 'sourceArray')'
                    // Does not much, the race condition without exception is the main issue being demonstrated here.
                    // Ignore this exception, to simplify the demonstration. But even if this exception didn't happen, items would still be lost.
                    droppedDueToException++;
                }
        });

        var expected = producers * PerProducer;                     // If a List<T> was thread-safe for writes, this is what we would expect.
        var actual = bag.Count;                                     // How many items were actually added.
        var dropped = expected - actual;                            // How many items were dropped in total.
        var droppedDueToRace = dropped - droppedDueToException;     // How many items were dropped due to the race condition

        //Print results
        Console.WriteLine($"Expected Item Count:  {expected}");
        Console.WriteLine($"Actual Item Count:    {actual}");
        Console.WriteLine($"Dropped Item Count:   {dropped}");
        Console.WriteLine($"Dropped by Exception: {droppedDueToException}");
        Console.WriteLine($"Dropped by Race:      {droppedDueToRace}");

        //ASSERT
        // We expect the count to be different.
        Assert.AreNotEqual(expected, actual, "We expected List<T>.Add to lose items due to race condition.");
    }

    [TestMethod]
    public void ConcurrentBag_is_thread_safe_for_writes()
    {
        //ARRANGE
        var bag = new ConcurrentBag<int>();

        //ACT
        Parallel.For(0, producers, _ =>
        {
            for (var i = 0; i < PerProducer; i++)
                bag.Add(i);
        });

        var expected = producers * PerProducer;
        var actual = bag.Count;
        var dropped = expected - actual;

        //Print results
        Console.WriteLine($"Expected Item Count:  {expected}");
        Console.WriteLine($"Actual Item Count:    {actual}");
        Console.WriteLine($"Dropped Item Count:   {dropped}");

        //ASSERT
        Assert.AreEqual(expected, actual, "We expected ConcurrentBag.Add to lose items due to race condition.");
    }
}