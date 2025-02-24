using System.Text.Json;
using System.Text.Json.Serialization;

namespace QuickTests.Json.TypeSpecific;

//[TestClass]
public class JsonArrayDeserializeTests
{
    const string json = @"[
        [
            ""2024-03-26T04:50:53.540000Z"",
            ""70181bde-8753-436c-9440-1abe74ecdc4a"",
            0
        ],
        [
            ""2024-03-26T04:50:53.540000Z"",
            ""70181bde-8753-436c-9440-1abe74ecdc4a"",
            1
        ],
        [
            ""2024-03-26T04:50:53.540000Z"",
            ""70181bde-8753-436c-9440-1abe74ecdc4a"",
            2
        ]
]";

    private const string jsonOneElement = @"[
            ""2024-03-26T04:50:53.540000Z"",
            ""70181bde-8753-436c-9440-1abe74ecdc4a"",
            3
        ]";
    private const int NumberOfRecords = 3;
    private const int NumberOfValues = 3;

    [TestMethod]
    public void TestMethod1()
    {
        //Arrange


        //Act
        var deser = JsonSerializer.Deserialize<JsonElement[][]>(json);

        //Assert
        Assert.IsNotNull(deser);
        Assert.AreEqual(NumberOfRecords, deser.Length);
        foreach (var record in deser)
        {
            Assert.IsNotNull(record);
            Assert.AreEqual(NumberOfValues, record.Length);
        }
    }

    [TestMethod]
    public void TestMethod2()
    {
        //Arrange


        //Act
        var deser = JsonSerializer.Deserialize<SomePoco>(jsonOneElement);

        //Assert
        Assert.IsNotNull(deser);

    }

    class SomePoco
    {
        [JsonPropertyOrder(0)]
        public DateTime Timestamp { get; set; }
        [JsonPropertyOrder(1)]
        public Guid Guid { get; set; }
        [JsonPropertyOrder(2)]
        public int Number { get; set; }
    }
}