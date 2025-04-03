using System.Text.Json.Serialization.Metadata;

namespace QuickTests.Json.Modifiers.TimeOfSerialization;

public static class MyModifiers
{
    public static void AddSerializationTime(JsonTypeInfo info) => AddSerializationTime(info, "$TimeOfSerialization");
    public static void AddSerializationTime(JsonTypeInfo info, string propertyName)
    {
        if (info.Kind != JsonTypeInfoKind.Object) return;

        //Create new Json Property
        var jpi = info.CreateJsonPropertyInfo(typeof(DateTime), propertyName);

        //Define how to retrieve the Property from the poco (even though we don't get the time from the poco)
        jpi.Get = _ => DateTime.Now;
        jpi.Set = null; //We will never read from Json and write to Poco...

        //Place as early as possible
        jpi.Order = int.MinValue;

        //Include in Json Type Info
        info.Properties.Add(jpi);
    }
}