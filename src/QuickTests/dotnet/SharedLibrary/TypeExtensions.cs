namespace SharedLibrary;

public static class TypeExtensions
{
    public static string GetFriendlyTypeName(this Type type)
    {
        if (!type.IsGenericType)
            return type.Name;

        var typeName = type.Name.Split('`')[0]; // Remove arity suffix
        var genericArgs = string.Join(", ", type.GetGenericArguments().Select(GetFriendlyTypeName));

        return $"{typeName}<{genericArgs}>";
    }
}