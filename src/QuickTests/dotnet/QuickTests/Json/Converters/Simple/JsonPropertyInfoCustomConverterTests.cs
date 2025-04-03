using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace QuickTests.Json.Converters.Simple;

/// <summary>
/// <see cref="JsonPropertyInfo.CustomConverter"/> related tests.
/// </summary>
/// <remarks>
/// We want to know when this property is set, and to what it is set.
/// In short: Attributes on properties cause this to be set.
/// When providing a converter (or factory) by options, it's not set to the property
/// </remarks>
[TestClass]
public class JsonPropertyInfoCustomConverterTests
{
    private static readonly JsonSerializerOptions DefaultJsonSerializerOptions = new(JsonSerializerOptions.Default)
    {
        WriteIndented = true,
    };
    private static void Test<T>(T data, JsonSerializerOptions options)
    {
        //Act
        var json = JsonSerializer.Serialize(data, options);
        var jti = options.GetTypeInfo(typeof(T));
        var propertyInfo = jti.Properties.Single();
        var converterType = propertyInfo.CustomConverter?.GetType();
        var converterTypeName = converterType is null ? "(null)" : $"{converterType.Namespace}.{converterType.Name}";

        //Assert
        Console.WriteLine($"PropertyInfo.CustomConverter: {converterTypeName}"); // <== This is what we want to know here
        Console.WriteLine(json);
    }

    [TestMethod]
    public void Default()
    {
        //Arrange
        var data = new SimplePoco();
        var options = DefaultJsonSerializerOptions;

        Test(data, options);
    }

    [TestMethod]
    public void AttributeOnProperty()
    {
        //Arrange
        var data = new SimplePocoWithAttributeOnProperty();
        var options = DefaultJsonSerializerOptions;

        Test(data, options);
    }
    [TestMethod]
    public void FactoryAttributeOnProperty()
    {
        //Arrange
        var data = new SimplePocoWithFactoryAttributeOnProperty();
        var options = DefaultJsonSerializerOptions;

        Test(data, options);
    }
    [TestMethod]
    public void ConverterInOptions()
    {
        //Arrange
        var data = new SimplePoco();
        var options = new JsonSerializerOptions(DefaultJsonSerializerOptions)
        {
            Converters = { new AlsText<MyEnum>() }
        };

        Test(data, options);
    }
    [TestMethod]
    public void ConverterFactoryInOptions()
    {
        //Arrange
        var data = new SimplePoco();
        var options = new JsonSerializerOptions(DefaultJsonSerializerOptions)
        {
            Converters = { new MyEnumJsonConverterFactory() }
        };

        Test(data, options);
    }
    [TestMethod]
    public void ConverterInModifier()
    {
        //Arrange
        var data = new SimplePoco();
        var options = new JsonSerializerOptions(DefaultJsonSerializerOptions)
        {
            TypeInfoResolver = new DefaultJsonTypeInfoResolver()
            {
                Modifiers =
                {
                    info =>
                    {
                        if (info.Type != typeof(SimplePoco)) return;
                        foreach (var item in info.Properties.Where(p => p.PropertyType == typeof(MyEnum)).ToList())
                            item.CustomConverter = new AlsText<MyEnum>();
                    }
                }
            }
        };

        Test(data, options);
    }
    [TestMethod]
    public void ConverterFactoryInModifier()
    {
        //Arrange
        var data = new SimplePoco();
        var options = new JsonSerializerOptions(DefaultJsonSerializerOptions)
        {
            TypeInfoResolver = new DefaultJsonTypeInfoResolver()
            {
                Modifiers =
                {
                    info =>
                    {
                        if (info.Type != typeof(SimplePoco)) return;
                        foreach (var item in info.Properties.Where(p => p.PropertyType == typeof(MyEnum)).ToList())
                            item.CustomConverter = new MyEnumJsonConverterFactory();
                    }
                }
            }
        };

        Test(data, options);
    }

    class SimplePoco
    {
        public MyEnum Value { get; set; }
    }
    class SimplePocoWithAttributeOnProperty
    {
        [JsonConverter(typeof(AlsText<MyEnum>))]
        public MyEnum Value { get; set; }
    }
    class SimplePocoWithFactoryAttributeOnProperty
    {
        [JsonConverter(typeof(AlsText<MyEnum>))]
        public MyEnum Value { get; set; }
    }

    class MyEnumJsonConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(MyEnum);
        }

        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return new AlsText<MyEnum>();
        }
    }
    class AlsText<T> : JsonConverter<T> where T : Enum
    {
        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

    enum MyEnum
    {
        A,
        B,
        C,
    }

    [JsonConverter(typeof(AlsText<MyEnumWithAttribute>))]
    enum MyEnumWithAttribute
    {
        A,
        B,
        C,
    }
}