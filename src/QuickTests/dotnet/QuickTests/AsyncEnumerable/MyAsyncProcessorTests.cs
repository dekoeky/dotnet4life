using SharedLibrary.AsyncEnumerable;

namespace QuickTests.AsyncEnumerable;

[TestClass]
public class MyAsyncProcessorTests
{
    [TestMethod]
    [DataRow(5, 10, 10)]
    [DataRow(5, 100, 100)]
    [DataRow(5, 1000, 1000)]
    public async Task TestMethod1(int n, int processingTime, int queryTime)
    {
        //ARRANGE
        var start = DateTime.Now;
        var totalTime = TimeSpan.Zero;
        var processor = new MyAsyncProcessor(processingTime, queryTime);

        //ACT
        await foreach (var data in processor.RetrieveAndProcessData(n))
        {
            Console.WriteLine($"""
                                 [Result {data.DatabaseData.Number}]
                                 Database Call Start: {(data.DatabaseData.DatabaseCallStart - start).TotalMilliseconds} ms
                                 Database Call End:   {(data.DatabaseData.DatabaseCallEnd - start).TotalMilliseconds} ms
                                 Processing Start:    {(data.ProcessingStart - start).TotalMilliseconds} ms
                                 Processing End:      {(data.ProcessingEnd - start).TotalMilliseconds} ms

                                 """);

            totalTime = data.ProcessingEnd - start;
        }

        Console.WriteLine($"Total Time: {totalTime.TotalMilliseconds}");

        //ASSERT

    }

    [TestMethod]
    [DataRow(5, 10, 10)]
    [DataRow(5, 100, 100)]
    [DataRow(5, 1000, 1000)]
    public async Task TestMethod2(int n, int processingTime, int queryTime)
    {
        //ARRANGE
        var start = DateTime.Now;
        var totalTime = TimeSpan.Zero;
        var processor = new MyAsyncProcessor(processingTime, queryTime);

        //ACT
        await foreach (var data in processor.RetrieveAndProcessDataWithChannel(n))
        {
            Console.WriteLine($"""
                               [Result {data.DatabaseData.Number}]
                               Database Call Start: {(data.DatabaseData.DatabaseCallStart - start).TotalMilliseconds} ms
                               Database Call End:   {(data.DatabaseData.DatabaseCallEnd - start).TotalMilliseconds} ms
                               Processing Start:    {(data.ProcessingStart - start).TotalMilliseconds} ms
                               Processing End:      {(data.ProcessingEnd - start).TotalMilliseconds} ms

                               """);

            totalTime = data.ProcessingEnd - start;
        }

        Console.WriteLine($"Total Time: {totalTime.TotalMilliseconds}");

        //ASSERT

    }
}