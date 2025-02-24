using System.Text.Json;
using System.Text.Json.Serialization;

namespace QuickTests.Json.Basics.Attributes;

[TestClass]
public class JsonPropertyOrderTests
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
        var a = new A();
        var b = new B();
        var c = new C();

        //Act
        var jsonA = JsonSerializer.Serialize(a, _options);
        var jsonB = JsonSerializer.Serialize(b, _options);
        var jsonC = JsonSerializer.Serialize(c, _options);

        //Assert
        Console.WriteLine($"A: {jsonA}");
        Console.WriteLine($"B: {jsonB}");
        Console.WriteLine($"C: {jsonC}");
    }

    [TestMethod]
    public void CombinedJsonPropertyOrderAttributeAndUnOrdered()
    {
        //The logic that seems to be used is:
        //PropertyInfo property;
        //var order = property.GetCustomAttribute<JsonPropertyOrderAttribute>()?.Order ?? 0;
        //and then order by order in poco (properties with same order, are ordered by order in poco)


        //Arrange
        var data = new D();

        //Act
        var json = JsonSerializer.Serialize(data, _options);

        //Assert
        Console.WriteLine(json);
    }



    private class A
    {
        public int PropertyOne { get; set; } = 1;
        public int PropertyTwo { get; set; } = 2;
        public int PropertyThree { get; set; } = 3;
    }

    private class B
    {
        public int PropertyThree { get; set; } = 3;
        public int PropertyTwo { get; set; } = 2;
        public int PropertyOne { get; set; } = 1;
    }

    private class C
    {
        [JsonPropertyOrder(3)]
        public int PropertyThree { get; set; } = 3;

        [JsonPropertyOrder(2)]
        public int PropertyTwo { get; set; } = 2;

        [JsonPropertyOrder(1)]
        public int PropertyOne { get; set; } = 1;
    }



    private class D
    {
        public int UnOrderedMinusThree { get; set; } = -3;
        [JsonPropertyOrder(-2)] public int MinusTwo { get; set; } = -2;
        public int UnOrderedMinusOneA { get; set; } = -1;
        [JsonPropertyOrder(-1)] public int MinusOne { get; set; } = -1;
        public int UnOrderedMinusOneB { get; set; } = -1;
        [JsonPropertyOrder(0)] public int Zero { get; set; } = 0;
        public int UnOrderedPlusOneA { get; set; } = +1;
        [JsonPropertyOrder(+1)] public int PlusOne { get; set; } = +1;
        public int UnOrderedPlusOneB { get; set; } = +1;
        [JsonPropertyOrder(+2)] public int PlusTwo { get; set; } = +2;
        public int UnOrderedPlusThree { get; set; } = +3;
    }
}