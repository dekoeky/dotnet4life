namespace PerformanceTests.Benchmarks.Json.SourceGenerators.MoreComplex.Models;

public class Order
{
    public int OrderId { get; set; }
    public List<OrderItem> Items { get; set; } = new();
    public decimal Total { get; set; }
}