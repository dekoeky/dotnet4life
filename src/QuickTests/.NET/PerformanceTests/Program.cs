using BenchmarkDotNet.Running;
using PerformanceTests.Benchmarks.DataTypes.Strings;

//var summary = BenchmarkRunner.Run<JsonSerializeBenchmarks>();
//var summary = BenchmarkRunner.Run<JsonDeSerializeBenchmarks>();
var summary = BenchmarkRunner.Run<CreateStringBenchmarks>();