using QuickTests._Helpers.TestExtensions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace QuickTests.Json.Basics.Attributes;

[TestClass]
public class JsonIgnoreTests
{
    private readonly JsonSerializerOptions _options = new()
    {
        //For readability
        WriteIndented = true,
    };

    [TestMethod]
    public void Serialize()
    {
        //Arrange
        var data = new MyData { A = 1, B = 2, };

        //Act
        var json = JsonSerializer.Serialize(data, _options);

        //Assert
        Console.WriteLine(json);
        var ja = Assert.That.IsJson(json);
        ja.ContainsProperty("A");
        ja.DoesNotContainProperties("B", "b");
    }

    [TestMethod]
    public void Deserialize()
    {
        //Arrange
        const string json = """
                   {
                     "A": 10,
                     "B": 20
                   }
                   """;

        //Act
        var data = JsonSerializer.Deserialize<MyData>(json, _options);

        //Assert
        Assert.IsNotNull(data);
        Assert.AreEqual(10, data.A);
        Assert.AreNotEqual(20, data.B);
    }

    private record MyData
    {
        public int A { get; init; }
        [JsonIgnore] public int B { get; init; }
    }
}