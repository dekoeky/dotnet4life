namespace PerformanceTests.Benchmarks.Json.SourceGenerators.MoreComplex.Models;

public class Address
{
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
    public string State { get; set; } = default!;
    public string PostalCode { get; set; } = default!;
    public Country Country { get; set; }
}