using BenchmarkDotNet.Attributes;

namespace PerformanceTests.Benchmarks.DataTypes.Strings
{
    public class CreateStringBenchmarks
    {
        [Benchmark]
        public string foo()
        {
            var s = "Hello";
            return s;
        }

        [Benchmark]
        public string ffaaoo()
        {
            string s = new string(['H', 'e', 'l', 'l', 'o']);
            return s;
        }
    }
}
