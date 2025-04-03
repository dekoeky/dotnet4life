using System.Text.Json.Serialization.Metadata;

namespace WebApplication1.Json.Modifiers;

public static class MyModifiers
{
    private const string ObjectTypeJsonPropertyName = "$TYPE";

    /// <summary>
    /// Adds a Json Property to the Json that shows the full type name.
    /// </summary>
    /// <param name="info"></param>
    public static void AddTypeName(JsonTypeInfo info)
    {
        if (info.Kind != JsonTypeInfoKind.Object) return;

        var prop = info.CreateJsonPropertyInfo(typeof(string), ObjectTypeJsonPropertyName);

        prop.Get = o => o.GetType().FullName;
        prop.Set = null;
        prop.Order = int.MinValue;

        info.Properties.Add(prop);
    }
}