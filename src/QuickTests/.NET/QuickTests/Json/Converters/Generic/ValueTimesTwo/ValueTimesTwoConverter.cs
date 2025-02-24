using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace QuickTests.Json.Converters.Generic.ValueTimesTwo;

/// <summary>
/// A simple JsonConverter for <see cref="INumber{TSelf}"/> values, that writes the double value into the Json
/// </summary>
/// <remarks>This converter is pure for educational purposes</remarks>
/// <typeparam name="T">The number type that is being serialized</typeparam>
public class ValueTimesTwoConverter<T> : JsonConverter<T> where T : INumber<T>
{
    private static readonly JsonConverter<T> DefaultConverter = (JsonConverter<T>)JsonSerializerOptions.Default.GetConverter(typeof(T));
    private static readonly T Multiplier = T.CreateChecked(2);

    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var jsonValue = DefaultConverter.Read(ref reader, typeToConvert, options)
            ?? throw new InvalidOperationException("Could not read value");

        return jsonValue / Multiplier;
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        DefaultConverter.Write(writer, value * Multiplier, options);
    }
}