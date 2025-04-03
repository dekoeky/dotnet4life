using System.Text.Json.Serialization;

namespace PerformanceTests.Benchmarks.Json.SourceGenerators.MoreComplex.Models;

[JsonSerializable(typeof(List<Customer>))]
public partial class CustomerJsonContext : JsonSerializerContext { }