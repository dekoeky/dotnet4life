using System.CodeDom.Compiler;
using System.Reflection;

namespace QuickTests.Reflection;

[TestClass]
public class ReflectionTests
{
    [DataTestMethod]
    [DataRow(typeof(Poco))]
    public void PrintProperties(Type type)
    {
        using var itw = new IndentedTextWriter(Console.Out);
        foreach (var propertyInfo in type.GetProperties())
            PrintProperty(itw, propertyInfo);
    }

    private static void PrintProperty(IndentedTextWriter itw, PropertyInfo propertyInfo)
    {
        itw.WriteLine(propertyInfo.Name);
        itw.Indent++;

        //Console.WriteLine(p.MemberType);
        itw.WriteLine(propertyInfo.PropertyType);
        //Console.WriteLine(p.DeclaringType);
        //Console.WriteLine(p.ReflectedType);
        itw.WriteLine($"Nullable: {Nullable.GetUnderlyingType(propertyInfo.PropertyType) is not null}");

        itw.Indent--;
        itw.WriteLine();
    }

    private class Poco
    {
        public int? NullableInt { get; set; }
        public int Int { get; set; }
        public int? ExplicitNullableInt { get; set; }

        public string String { get; set; }

        public string? NullableString { get; set; }
    }
}