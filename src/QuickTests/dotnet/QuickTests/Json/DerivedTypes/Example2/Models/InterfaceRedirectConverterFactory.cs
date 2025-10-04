using System.Text.Json;
using System.Text.Json.Serialization;

namespace QuickTests.Json.DerivedTypes.Example2.Models;

internal class InterfaceRedirectConverterFactory<T, TBase>
    : JsonConverterFactory
    where T : TBase
{
    public override bool CanConvert(Type typeToConvert)
    {
        try
        {
            return typeof(T).IsAssignableFrom(typeToConvert);
        }
        catch
        {
            return false;
        }
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        return new SerializeAsBaseTypeJsonConverter<T, TBase>();
        //var redirectConverter = (JsonConverter)Activator.CreateInstance(
        //    typeof(SerializeAsBaseTypeJsonConverter<,>).MakeGenericType(typeof(T), typeof(TBase))
        //);
        //return redirectConverter;

        //return new InterfaceRedirectConverter();
    }
}