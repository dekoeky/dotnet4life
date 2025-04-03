using System.Text.Json;

namespace ClassLibraryNetStandard21
{
    public static class Class21JsonExtensions
    {
        private static readonly JsonSerializerOptions Indented = new JsonSerializerOptions(JsonSerializerOptions.Default)
        {
            WriteIndented = true
        };
        private static readonly JsonSerializerOptions Regular = new JsonSerializerOptions(JsonSerializerOptions.Default)
        {
            WriteIndented = false
        };

        public static string ToJson(this Class21 class1, bool indented = true)
            => JsonSerializer.Serialize(class1, indented ? Indented : Regular);
    }
}