using System.Reflection;

namespace QuickTests.DataTypes.Enums.Flags;

[AttributeUsage(AttributeTargets.Method)]
public class TestForAllEnumValues : Attribute, ITestDataSource
{
    private const string CouldNotDetermineEnumType = "Could not determine enum type";
    public IEnumerable<object?[]> GetData(MethodInfo methodInfo)
    {
        var enumType = ExtractEnumType(methodInfo) ?? throw new InvalidOperationException(CouldNotDetermineEnumType);

        var values = Enum.GetValues(enumType);

        var zeroWasTested = false;

        foreach (var value in values)
        {
            yield return [value];
            if (Convert.ToInt32(value) == 0) zeroWasTested = true;
        }

        if (!zeroWasTested) yield return [0];
    }

    /// <summary>
    /// Extracts the enum type from the method parameters.
    /// </summary>
    private static Type? ExtractEnumType(MethodInfo methodInfo) =>
        methodInfo.GetParameters()
            //Assume the first enum type is the correct type
            .FirstOrDefault(t => t.ParameterType.IsEnum)
            //We are interested in the type of the parameter
            ?.ParameterType;


    public string? GetDisplayName(MethodInfo methodInfo, object?[]? data)
        => data is { Length: > 0 }
            ? $"{methodInfo.Name} ({data[0]})"
            : ExtractEnumType(methodInfo) is { } enumType
                ? $"{methodInfo.Name} ({enumType.Name})"
                : $"{methodInfo.Name} ({CouldNotDetermineEnumType})";
}