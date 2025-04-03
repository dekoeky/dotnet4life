using BenchmarkDotNet.Attributes;
using PerformanceTests.Benchmarks.Json.SourceGenerators.MoreComplex.Models;
using System.Text.Json;

namespace PerformanceTests.Benchmarks.Json.SourceGenerators.MoreComplex;

[MemoryDiagnoser]
public class JsonSerializeBenchmarks
{

    private static readonly List<Customer> Customers = GenerateSampleData(1000);
    private static readonly JsonSerializerOptions DefaultOptions = new JsonSerializerOptions();

    [Benchmark]
    public string BenchmarkDefaultSerializer() => JsonSerializer.Serialize(Customers, DefaultOptions);

    [Benchmark]
    public string BenchmarkSourceGeneratedSerializer() => JsonSerializer.Serialize(Customers, Models.CustomerJsonContext.Default.ListCustomer);


    private static List<Customer> GenerateSampleData(int count)
    {
        var customers = new List<Customer>();
        for (int i = 0; i < count; i++)
        {
            customers.Add(new Customer
            {
                Id = i + 1,
                Name = $"Customer {i + 1}",
                CreatedDate = DateTime.UtcNow,
                Address = new Address
                {
                    Street = $"123 Main St {i + 1}",
                    City = "City",
                    State = "State",
                    PostalCode = $"12345-{i:D4}",
                    Country = Country.USA
                },
                Orders = new List<Order>
                {
                    new Order
                    {
                        OrderId = i + 1000,
                        Total = 99.99m,
                        Items = new List<OrderItem>
                        {
                            new OrderItem
                            {
                                ProductId = i + 5000,
                                ProductName = "Product A",
                                Quantity = 2,
                                Price = 49.99m
                            }
                        }
                    }
                }
            });
        }
        return customers;
    }
}