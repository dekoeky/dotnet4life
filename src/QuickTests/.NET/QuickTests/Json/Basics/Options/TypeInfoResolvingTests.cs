using QuickTests.TestModels;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace QuickTests.Json.Basics.Options;

[TestClass]
public class TypeInfoResolvingTests
{
    [DataTestMethod]
    [DynamicData(nameof(GetOptions))]
    public void ResolveTypeInfoFromOptions(bool expectedToBeResolvable, JsonSerializerOptions options)
    {
        //ARRANGE
        var t = typeof(WeatherForecast);

        //ACT
        var resolvable = options.TryGetTypeInfo(t, out var jti);
        //var hasResolver = options.TypeInfoResolver is not null;

        //ASSERT
        if (expectedToBeResolvable)
        {
            Assert.IsTrue(resolvable);
            Assert.IsNotNull(jti);
            Assert.AreNotEqual(JsonTypeInfoKind.None, jti.Kind);
        }
        else
        {
            Assert.IsFalse(resolvable);
            Console.WriteLine("This JsonSerializerOptions is indeed not able to resolve the info");
        }
    }

    public static IEnumerable<object[]> GetOptions
    {
        get
        {
            yield return [true, JsonSerializerOptions.Default];
            yield return [true, new JsonSerializerOptions(JsonSerializerOptions.Default)];
            yield return [false, new JsonSerializerOptions()];
            yield return [true, new JsonSerializerOptions
            {
                TypeInfoResolver = new DefaultJsonTypeInfoResolver()
            }];

        }
    }
}