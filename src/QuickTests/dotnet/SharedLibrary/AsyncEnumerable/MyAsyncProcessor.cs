using System.Threading.Channels;

namespace SharedLibrary.AsyncEnumerable;

internal class MyAsyncProcessor(int processingTime, int queryTime)
{
    private readonly TimeSpan _processingTime = TimeSpan.FromMilliseconds(processingTime);
    private readonly TimeSpan _queryTime = TimeSpan.FromMilliseconds(queryTime);

    private async IAsyncEnumerable<DatabaseData> GetAllDataFromDatabase(int n)
    {
        for (var i = 0; i < n; i++)
            yield return await SingleDataFromDatabase(i);
    }

    private async Task<DatabaseData> SingleDataFromDatabase(int i)
    {
        // Mark the start time
        var start = DateTime.Now;

        // Simulate a 'slow' db call
        await Task.Delay(_queryTime);

        // Mark the end time
        var end = DateTime.Now;

        // Return test data
        return new DatabaseData
        {
            Number = i,
            DatabaseCallStart = start,
            DatabaseCallEnd = end,
        };
    }

    private async Task<ProcessingData> ProcessSingleData(DatabaseData data)
    {
        // Mark the time the processing started
        var start = DateTime.Now;

        // Simulate the 'low' processing
        await Task.Delay(_processingTime);

        // Mark the time the processing finished
        var end = DateTime.Now;

        // return test data
        return new ProcessingData
        {
            DatabaseData = data,
            ProcessingStart = start,
            ProcessingEnd = end,
        };
    }

    public async IAsyncEnumerable<ProcessingData> RetrieveAndProcessData(int n)
    {
        await foreach (var data in GetAllDataFromDatabase(n))
            yield return await ProcessSingleData(data);
    }


    public async IAsyncEnumerable<ProcessingData> RetrieveAndProcessDataWithChannel(int n)
    {
        var channel = Channel.CreateUnbounded<DatabaseData>();

        // Producer: fetch from DB and enqueue
        var producer = Task.Run(async () =>
        {
            await foreach (var data in GetAllDataFromDatabase(n))
                await channel.Writer.WriteAsync(data);

            channel.Writer.Complete();
        });

        // Consumer: read from channel and process
        await foreach (var data in channel.Reader.ReadAllAsync())
            yield return await ProcessSingleData(data);

        await producer; // Ensure producer finishes
    }
}