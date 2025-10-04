using System.Text.Json;

namespace QuickTests._Helpers.TestExtensions;

public static class JsonAssertExtensions
{
    public static JsonAssert IsJson(this Assert assert, string json)
    {
        try
        {
            return new JsonAssert(JsonDocument.Parse(json));
        }
        catch (Exception e)
        {
            throw new AssertFailedException("The given string was not valid Json", e);
        }
    }
}