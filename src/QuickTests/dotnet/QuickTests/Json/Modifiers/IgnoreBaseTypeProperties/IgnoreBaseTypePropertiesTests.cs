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
        Assert.DoesNotContain("__A__", json); //This property is defined in a base class of C (T Ignore) and is expected not to be serialized
        Assert.DoesNotContain("__B__", json); //This property is defined in a base class of C (T Ignore) and is expected not to be serialized
        Assert.DoesNotContain("__C__", json); //This property is defined in C (T Ignore) and is expected not to be serialized
        Assert.Contains("__D__", json);  //This property is defined in a subclass of C (T Ignore) and is expected to be serialized
        Assert.Contains("__E__", json);  //This property is defined in a subclass of C (T Ignore) and is expected to be serialized
    }
}