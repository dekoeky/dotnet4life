namespace PerformanceTests.Benchmarks.Json.SourceGenerators.MoreComplex.Models;

public class OrderItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}