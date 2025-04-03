using System.Text.Json.Serialization.Metadata;

namespace QuickTests.Json.Modifiers.IgnoreBaseTypeProperties;

public static class JsonModifiers
{
    /// <summary>
    /// Ignore Json Properties defined in type <typeparamref name="TIgnore"/> (and base classes) for inheriting types/>.
    /// </summary>
    /// <param name="info"></param>
    public static void IgnoreBaseTypeProperties<TIgnore>(JsonTypeInfo info)
    {
        if (info.Kind != JsonTypeInfoKind.Object)
            return;

        //We only want to modify Json Serialization for types that inherit from TIgnore
        if (!info.Type.IsSubclassOf(typeof(TIgnore)))
            return;

        //We don't want to modify the serialization of the base type
        if (info.Type == typeof(TIgnore))
            return;

        //Loop existing Json Properties (backwards for modifying the collection)
        for (var i = info.Properties.Count - 1; i >= 0; i--)
        {
            var property = info.Properties[i];

            //If the property is declared in a type that is a subclass of TIgnore, 
            //we are sure it's not defined in TIgnore (or lower)
            if (property.DeclaringType.IsSubclassOf(typeof(TIgnore)))
                continue;

            //This property is defined in TIgnore (or one of its base classes) and should be removed
            info.Properties.RemoveAt(i);
        }
    }
}