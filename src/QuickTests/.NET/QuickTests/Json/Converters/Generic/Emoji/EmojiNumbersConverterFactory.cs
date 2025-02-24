using System.Numerics;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace QuickTests.Json.Converters.Generic.Emoji;

internal class EmojiNumbersConverterFactory : JsonConverterFactory
{
    public static bool IsNumber<T>() => IsNumber(typeof(T));

    private static bool IsNumber(Type ttt)
    {
        try
        {
            return typeof(INumber<>).MakeGenericType(ttt).IsAssignableFrom(ttt);
        }
        catch
        {
            return false;
        }
    }

    public override bool CanConvert(Type typeToConvert)
    {
        var isNumber = IsNumber(typeToConvert);

        //Can only convert numbers
        return isNumber;
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) =>
        (JsonConverter)Activator.CreateInstance(
            typeof(EmojiNumbersConverter<>).MakeGenericType(typeToConvert),
            BindingFlags.Instance | BindingFlags.Public,
            binder: null,
            args: null,
            culture: null)!;
}