using SharedLibrary.Reflection.Assignables;
using System.Reflection;

namespace QuickTests.Reflection.Assignables;

[AttributeUsage(AttributeTargets.Method)]
public class AssignablesTestDataSourceAttribute : Attribute, ITestDataSource
{
    public IEnumerable<object?[]> GetData(MethodInfo methodInfo)
    {
        foreach (var (type, baseType, assignable) in AssignablesTestData.Get())
            yield return [type, baseType, assignable];
    }


    public string GetDisplayName(MethodInfo methodInfo, object?[]? data) =>
        $"{methodInfo.Name} ({data![0]} -> {data[1]})";
}
