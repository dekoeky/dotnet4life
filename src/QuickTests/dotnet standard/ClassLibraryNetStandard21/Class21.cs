using System.Reflection;

namespace ClassLibraryNetStandard21
{
    public class Class21
    {
        private static readonly string AssemblyName = Assembly.GetAssembly(typeof(Class21)).FullName;
        public string SomeMessage { get; set; } = $"Message Generated in {AssemblyName}";
    }
}
