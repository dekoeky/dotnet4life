using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization.Metadata;

namespace QuickTests.Json.Modifiers.TimeOfDeserialization;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public static class MyModifiers
{
    public static void ApplyDeserializationTime<TInstance>(JsonTypeInfo info, Action<TInstance, DateTime> propertySetter)
        where TInstance : class
    {
        if (info.Type != typeof(TInstance)) return;

        info.OnDeserialized += o =>
        {
            if (o is TInstance instance)
                propertySetter(instance, DateTime.Now);
        };
    }

    public static Action<JsonTypeInfo> ApplyDeserializationTime<TInstance>(Action<TInstance, DateTime> propertySetter)
        where TInstance : class
    {
        return info => ApplyDeserializationTime(info, propertySetter);
    }

}