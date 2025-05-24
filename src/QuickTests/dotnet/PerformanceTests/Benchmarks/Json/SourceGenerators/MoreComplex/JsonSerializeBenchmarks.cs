using BenchmarkDotNet.Attributes;
using PerformanceTests.Benchmarks.Json.SourceGenerators.MoreComplex.Models;
using System.Text.Json;

namespace PerformanceTests.Benchmarks.Json.SourceGenerators.MoreComplex;

[MemoryDiagnoser]
public class JsonSourceGeneratedSerializedBenchmarks
{
    private static readonly List<Customer> Customers = GenerateSampleData(1000);
    private static readonly JsonSerializerOptions DefaultOptions = JsonSerializerOptions.Default;
    private string json;


    [GlobalSetup]
    public void Setup()
    {
        var assembly = typeof(JsonSourceGeneratedSerializedBenchmarks).Assembly;
        using var stream = assembly.GetManifestResourceStream("PerformanceTests.Benchmarks.Json.SourceGenerators.MoreComplex.testdata.json")
            ?? throw new InvalidOperationException("Can't load testdata");
        using var streamReader = new StreamReader(stream);
        json = streamReader.ReadToEnd();
    }

    [Benchmark]
    public string SerializeDefault() => JsonSerializer.Serialize(Customers, DefaultOptions);

    [Benchmark]
    public string SerializeSourceGenerated() => JsonSerializer.Serialize(Customers, CustomerJsonContext.Default.ListCustomer);

    [Benchmark]
    public List<Customer>? DeserializeDefault() => JsonSerializer.Deserialize<List<Customer>>(json, DefaultOptions);

    [Benchmark]
    public List<Customer>? DeserializeSourceGenerated() => JsonSerializer.Deserialize(json, CustomerJsonContext.Default.ListCustomer);


    private static List<Customer> GenerateSampleData(int count)
    {
        var customers = new List<Customer>();
        for (var i = 0; i < count; i++)
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
                Orders =
                [
                    new Order
                    {
                        OrderId = i + 1000,
                        Total = 99.99m,
                        Items =
                        [
                            new OrderItem
                            {
                                ProductId = i + 5000,
                                ProductName = "Product A",
                                Quantity = 2,
                                Price = 49.99m
                            }
                        ]
                    }
                ]
            });
        }

        return customers;
    }
}