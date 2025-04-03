namespace PerformanceTests.Benchmarks.Json.SourceGenerators.MoreComplex.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public DateTime CreatedDate { get; set; }
    public Address Address { get; set; } = default!;
    public List<Order> Orders { get; set; } = new();
}