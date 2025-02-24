using System.Text.Json;

namespace QuickTests.Json.TypeSpecific
{
    [TestClass]
    public class BooleanJsonTests
    {
        [DataTestMethod]
        [DataRow("true")]
        [DataRow("false")]

        public void DeSerialize_ShouldWork(string value) => Execute(value);

        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        [DataTestMethod]
        [DataRow("True")]
        [DataRow("False")]
        [DataRow("\"True\"")]
        [DataRow("\"False\"")]
        [DataRow("0")]
        [DataRow("0.0")]
        [DataRow("1")]
        [DataRow("1.0")]
        [DataRow("2")]
        [DataRow("2.0")]
        public void DeSerialize_ShouldFail(string value) => Execute(value);

        private static void Execute(string value)
        {
            //Arrange
            var json =
                $$"""
                  {
                      "MyBool": {{value}}
                  }
                  """;
            Console.WriteLine(json);

            //Act
            var parsedValue = bool.Parse(value);
            var jsonDeserialized = JsonSerializer.Deserialize<MyPoco>(json);

            //Assert
            Console.WriteLine($"Result from bool.Parse :{parsedValue}");
            Console.WriteLine($"Result from Json Deserialization: {jsonDeserialized?.MyBool}");
        }

        private class MyPoco
        {
            public bool MyBool { get; init; }
        }
    }
}
