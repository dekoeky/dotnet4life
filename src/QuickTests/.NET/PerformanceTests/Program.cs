using BenchmarkDotNet.Running;
using PerformanceTests.Benchmarks.DataTypes.Strings;
using PerformanceTests.Benchmarks.Json;

//var summary = BenchmarkRunner.Run<JsonSerializeBenchmarks>();
//var summary = BenchmarkRunner.Run<JsonDeSerializeBenchmarks>();
var summary = BenchmarkRunner.Run<WWW>();