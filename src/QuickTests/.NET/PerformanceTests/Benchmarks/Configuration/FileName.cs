using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Configuration;

namespace PerformanceTests.Benchmarks.Configuration;

[ShortRunJob]
public class FileName
{
    private IConfiguration configuration = new ConfigurationBuilder()
        .AddInMemoryCollection(new Dictionary<string, string>()
        {
            { "MyValue", "true"}
        })
        .Build();

    [Params(true, false, null)]
    public bool? N;

    private int[] data;

    //[GlobalSetup]
    //public void GlobalSetup()
    //{
    //    var builder = new ConfigurationBuilder();

    //    if (N is not null)
    //        builder.AddInMemoryCollection(new Dictionary<string, string>())

    //    configuration = N is null ?  : new ConfigurationBuilder()
    //        .AddInMemoryCollection(new Dictionary<string, string>()
    //        {
    //            { "MyValue", N?.ToString()}
    //        })
    //        .Build();
    //}

}