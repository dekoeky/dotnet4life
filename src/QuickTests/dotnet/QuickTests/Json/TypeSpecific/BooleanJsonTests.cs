using System.Text.Json;

namespace QuickTests.Json.TypeSpecific;

[TestClass]
public class BooleanJsonTests
{
    [TestMethod]
    [DataRow("true", true)]
    [DataRow("false", false)]
    [DataRow("True")]
    [DataRow("False")]
    [DataRow("0")]
    [DataRow("1")]
    public void Deserialize(string json, bool? expectedResult = null)
    {
        //Arrange
        var expectedToFail = expectedResult is null;

        try
        {
            //Act
            var result = JsonSerializer.Deserialize<bool>(json);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }
        catch (JsonException)
        {
            if (!expectedToFail) throw;
        }
    }

    [TestMethod]
    [DataRow(true, "true")]
    [DataRow(false, "false")]
    public void Serialize(bool value, string expectedJson)
    {
        //Act
        var json = JsonSerializer.Serialize(value);

        //Assert
        Assert.AreEqual(expectedJson, json);
    }
}