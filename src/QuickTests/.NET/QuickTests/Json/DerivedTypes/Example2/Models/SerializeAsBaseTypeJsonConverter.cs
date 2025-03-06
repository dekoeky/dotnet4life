using System.Text.Json;
using System.Text.Json.Serialization;

namespace QuickTests.Json.DerivedTypes.Example2.Models;

public class SerializeAsBaseTypeJsonConverter<T, TBase> : JsonConverter<T> where T : TBase
{
    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        //return (T)JsonSerializer.Deserialize(ref reader, typeof(TBase), options);
        return (T?)JsonSerializer.Deserialize<TBase>(ref reader, options);
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        //JsonSerializer.Serialize(writer, (TBase)value, options);
        JsonSerializer.Serialize<TBase>(writer, value, options);
    }
}