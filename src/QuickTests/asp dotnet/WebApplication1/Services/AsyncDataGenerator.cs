namespace WebApplication1.Services;

internal class AsyncDataGenerator(Random random)
{
    /// <typeparam name="T">The resulting data type.</typeparam>
    /// <param name="random">The <see cref="Random"/> instance for random data.</param>
    /// <param name="i">The index of the data to generate.</param>
    public delegate T ConstructOne<T>(Random random, int i);

    public int StartDelay { get; set; } = 100;
    public int RowDelay { get; set; } = 0;

    public async IAsyncEnumerable<T> GenerateAsync<T>(ConstructOne<T> c, int n)
    {
        await Task.Delay(StartDelay);

        for (var i = 0; i < n; i++)
        {
            await Task.Delay(RowDelay);
            yield return c(random, i);
        }
    }
}