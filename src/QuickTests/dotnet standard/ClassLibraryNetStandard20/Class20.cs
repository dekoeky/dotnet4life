using System.Reflection;

namespace ClassLibraryNetStandard20
{
    public class Class20
    {
        private static readonly string AssemblyName = Assembly.GetAssembly(typeof(Class20)).FullName;
        public string SomeMessage { get; set; } = $"Message Generated in {AssemblyName}";
    }
}
