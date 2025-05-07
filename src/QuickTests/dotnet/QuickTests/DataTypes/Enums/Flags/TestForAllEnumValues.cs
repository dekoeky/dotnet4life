using System.Reflection;

namespace QuickTests.DataTypes.Enums.Flags;

[AttributeUsage(AttributeTargets.Method)]
public class TestForAllEnumValues : Attribute, ITestDataSource
{
    public IEnumerable<object?[]> GetData(MethodInfo methodInfo)
    {
        var firstParameter = methodInfo.GetParameters().First();
        var enumType = firstParameter.ParameterType;

        var values = Enum.GetValues(enumType);

        bool zeroWasTested = false;

        foreach (var value in values)
        {
            yield return [value];
            //if (Convert.ToInt32(value) == 0) zeroWasTested = true;
        }

        if (!zeroWasTested) yield return [0];
    }


    public string? GetDisplayName(MethodInfo methodInfo, object?[]? data) => $"{methodInfo.Name} ({data[0]})";
}