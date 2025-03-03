using QuickTests.DataTypes.Comparisons.Custom.TestModels;
using System.Globalization;
using System.Reflection;

namespace QuickTests.DataTypes.Comparisons.Custom.TestData;

[AttributeUsage(AttributeTargets.Method)]
public class OverridableIoFinalValueComparerTestDataAttribute : Attribute, ITestDataSource
{
    public IEnumerable<object[]> GetData(MethodInfo methodInfo)
    {
        yield return
        [
            true, //Equal because same final (actual) value
            new OverridableIo<int> { ActualValue = 1, OverrideValue = 2, IsOverriden = false },
            new OverridableIo<int> { ActualValue = 1, OverrideValue = 3, IsOverriden = false }
        ];
        yield return
        [
            false , //Not Equal because different (overridden) final values
            new OverridableIo<int> { ActualValue = 1, OverrideValue = 2, IsOverriden = true },
            new OverridableIo<int> { ActualValue = 1, OverrideValue = 3, IsOverriden = true  }
        ];
        yield return
        [
            false, //Not Equal because not same final (actual) value
            new OverridableIo<int> { ActualValue = 1, OverrideValue = 2, IsOverriden = false },
            new OverridableIo<int> { ActualValue = 5, OverrideValue = 3, IsOverriden = false }
        ];
        yield return
        [
            true, //Equal because actual and overriden = same
            new OverridableIo<int> { ActualValue = 1, OverrideValue = 2, IsOverriden = true },
            new OverridableIo<int> { ActualValue = 2, OverrideValue = 5, IsOverriden = false }
        ];
    }

    public string GetDisplayName(MethodInfo methodInfo, object?[]? data)
    {
        if (data is null || data.Length == 0) return methodInfo.Name;

        return string.Format(CultureInfo.CurrentCulture, "{0} ({1})", methodInfo.Name, string.Join(",", data));
    }
}