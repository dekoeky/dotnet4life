
#pragma warning disable CA1822
// ReSharper disable UseNameOfInsteadOfTypeOf
// ReSharper disable UseSymbolAlias
// ReSharper disable EntityNameCapturedOnly.Local

//An alias with a short and more readable name than the long original class name
using Ez = QuickTests.Generics.NameOfTests.SomeClassWithAnAnnoyingName;

namespace QuickTests.Generics;

[TestClass]
public class NameOfTests
{
    /// <summary>
    /// Demonstrates the use of `nameof` operator in various contexts.
    /// </summary>
    [TestMethod]
    public void Demo()
    {
        //Arrange
        const int padding = 42;
        const int someLocalConst = 100;

        //Act
        var results = new Dictionary<string, string>
        {
            { "nameof(field)",                         nameof(_someField) },
            { "nameof(static field)",                  nameof(_someStaticField) },
            { "nameof(const)",                         nameof(SomeConst) },
            { "nameof(local const)",                   nameof(someLocalConst) },
            { "nameof(enum value)",                    nameof(SomeEnum.SomeEnumValue) },
            { "nameof(method)",                        nameof(SomeMethod) },
            { "nameof(static method)",                 nameof(SomeStaticMethod) },
            { "nameof(namespace)",                     nameof(QuickTests.Generics)},
            { "nameof(local method)",                  nameof(SomeLocalMethod) },
            { "nameof(local static method)",           nameof(SomeLocalStaticMethod) },
            { "nameof(method parameter)",              NameOfParameter("test") },
            { "nameof(local method parameter)",        NameOfLocalMethodParameter("test") },
            { "nameof(generic method type parameter)", NameOfGenericMethodTypeParameter<int>() },
            { "nameof(generic class type parameter)",  SomeGenericClass<int>.ParameterName },
        };

        //Assert
        foreach (var (key, value) in results)
            Console.WriteLine($"{key + ':',-padding}{value}");

        return;

        void SomeLocalMethod() { }
        static void SomeLocalStaticMethod() { }
        string NameOfLocalMethodParameter(string someParameter) => nameof(someParameter);
    }

    [TestMethod]
    public void NameOfTypeAlias()
    {
        //Arrange
        const int padding = 42;

        //Act
        var results = new Dictionary<string, string>
        {
            { $"typeof({nameof(SomeClassWithAnAnnoyingName)}).Name",
                typeof(SomeClassWithAnAnnoyingName).Name },

            { $"typeof({nameof(Ez)}).Name",
                typeof(Ez).Name },

            { $"nameof({nameof(SomeClassWithAnAnnoyingName)})",
                nameof(SomeClassWithAnAnnoyingName) },

            //This is the only 'weird' one, because the name of the alias is returned
            //Not sure where to use this
            { $"nameof({nameof(Ez)})",
                nameof(Ez) }
        };

        //Assert
        foreach (var (key, value) in results)
            Console.WriteLine($"{key + ':',-padding}{value}");
    }

    internal class SomeClassWithAnAnnoyingName;

    private const int SomeConst = 42;
    private int _someField = SomeConst;
    private static int _someStaticField = SomeConst;
    private void SomeMethod() { }
    private static void SomeStaticMethod() { }
    private static string NameOfParameter(string someParameter) => nameof(someParameter);
    private static string NameOfGenericMethodTypeParameter<TSomeGenericMethodTypeParameter>() => nameof(TSomeGenericMethodTypeParameter);
    private static class SomeGenericClass<TSomeGenericClassTypeParameter>
    {
        public const string ParameterName = nameof(TSomeGenericClassTypeParameter);
    }
    private enum SomeEnum { SomeEnumValue }

    [TestMethod]
    public void NameOfGenericClass()
    {
        Console.WriteLine($"nameof(SomeGenericClass<int>):       {nameof(SomeGenericClass<int>)}");
        Console.WriteLine("nameof(SomeGenericClass):            not possible :(");
        Console.WriteLine();

        Console.WriteLine($"typeof(SomeGenericClass<>).Name:     {typeof(SomeGenericClass<>).Name}");
        Console.WriteLine($"typeof(SomeGenericClass<>).FullName: {typeof(SomeGenericClass<>).FullName}");
        Console.WriteLine();

        Console.WriteLine($"typeof(SomeGenericClass<>).Name:     {typeof(SomeGenericClass<int>).Name}");
        Console.WriteLine($"typeof(SomeGenericClass<>).FullName: {typeof(SomeGenericClass<int>).FullName}");
    }
}
