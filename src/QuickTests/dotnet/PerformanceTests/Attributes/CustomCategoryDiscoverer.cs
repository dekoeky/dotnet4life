using BenchmarkDotNet.Running;
using System.Reflection;

namespace PerformanceTests.Attributes;

internal class CustomCategoryDiscoverer : DefaultCategoryDiscoverer
{
    public CustomCategoryDiscoverer()
    {
        Console.WriteLine($"Hello world from {nameof(CustomCategoryDiscoverer)}");
    }
    public override string[] GetCategories(MethodInfo method)
    {
        //Get the (final reflected) type to which this test belongs
        var type = method.ReflectedType;

        var categories = new List<string>();
        categories.AddRange(base.GetCategories(method));
        var x = GetNamespaceParts(type?.Namespace).ToArray();
        categories.AddRange(x);
        return categories.ToArray();
    }

    private static IEnumerable<string> GetNamespaceParts(string? ns)
    {
        while (!string.IsNullOrEmpty(ns))
        {
            yield return ns;
            var lastDotIndex = ns.LastIndexOf('.');
            if (lastDotIndex == -1) break; // No more dots, we're done

            ns = ns[..lastDotIndex]; // Remove the last part
        }
    }
}