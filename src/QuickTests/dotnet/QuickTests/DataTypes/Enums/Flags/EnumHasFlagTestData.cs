using System.Reflection;

namespace QuickTests.DataTypes.Enums.Flags;

[AttributeUsage(AttributeTargets.Method)]
public abstract class EnumHasFlagTestData<T> : Attribute, ITestDataSource where T : struct, Enum
{
    public IEnumerable<object?[]> GetData(MethodInfo methodInfo) => GetTestData().Select(TupleExtensions.ToObjectArray);

    protected abstract IEnumerable<(T Value, T FlagToCheck, bool ExpectedResult)> GetTestData();

    public string? GetDisplayName(MethodInfo methodInfo, object?[]? data)
    {
        var s = methodInfo.Name;

        if (data is null) return s;


        if (data.Length > 0
            && data[0] is T value
            && data[1] is T flagToCheck
            && data[2] is bool expectedResult)
        {
            return expectedResult
                    ? $"{typeof(T).Name} {value} should NOT have flag {flagToCheck}"
                    : $"{typeof(T).Name} {value} SHOULD     have flag {flagToCheck}";
        }

        s += $" ({string.Join(", ", data)})";

        return s;
    }
}