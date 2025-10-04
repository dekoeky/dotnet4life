using System.Text.Json;

namespace QuickTests._Helpers.TestExtensions;

public class JsonAssert(JsonDocument json)
{
    public void ContainsProperty(string propertyName)
    {
        if (!json.RootElement.TryGetProperty(propertyName, out _))
            throw new AssertFailedException($"Json did not contain property {propertyName}");
    }

    public void ContainsProperties(params string[] propertyNames)
    {
        foreach (var propertyName in propertyNames)
            if (!json.RootElement.TryGetProperty(propertyName, out _))
                throw new AssertFailedException($"Json did not contain property {propertyName}");
    }

    public void DoesNotContainProperties(params string[] propertyNames)
    {
        foreach (var propertyName in propertyNames)
            if (json.RootElement.TryGetProperty(propertyName, out _))
                throw new AssertFailedException($"Json contains property {propertyName}");
    }
}