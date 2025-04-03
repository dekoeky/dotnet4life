using QuickTests.Json.Modifiers.IgnoreBaseTypeProperties.Models;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace QuickTests.Json.Modifiers.IgnoreBaseTypeProperties;

[TestClass]
public class IgnoreBaseTypePropertiesTests
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        WriteIndented = true,
        TypeInfoResolver = new DefaultJsonTypeInfoResolver
        {
            Modifiers =
            {
                JsonModifiers.IgnoreBaseTypeProperties<C>,
            }
        }
    };

    [TestMethod]
    public void IgnoreBaseTypeProperties()
    {
        //Arrange

        var data = new E();

        //Act
        var json = JsonSerializer.Serialize(data, JsonSerializerOptions);

        //Assert
        Console.WriteLine(json);
        Assert.IsFalse(json.Contains("__A__")); //This property is defined in a base class of C (T Ignore) and is expected not to be serialized
        Assert.IsFalse(json.Contains("__B__")); //This property is defined in a base class of C (T Ignore) and is expected not to be serialized
        Assert.IsFalse(json.Contains("__C__")); //This property is defined in C (T Ignore) and is expected not to be serialized
        Assert.IsTrue(json.Contains("__D__"));  //This property is defined in a subclass of C (T Ignore) and is expected to be serialized
        Assert.IsTrue(json.Contains("__E__"));  //This property is defined in a subclass of C (T Ignore) and is expected to be serialized
    }
}