using MyFavoriteClass = QuickTests.Generics.SomeClassWithAnAnnoyingName;

namespace QuickTests.Generics;

[TestClass]
public class NameOfTests
{
    [TestMethod]
    public void NameOfTypeAlias()
    {
        // ReSharper disable once UseNameOfInsteadOfTypeOf
        // ReSharper disable once InconsistentNaming
        var typeOfMyFavoriteClass_Name = typeof(MyFavoriteClass).Name;
        const string nameOfMyFavoriteClass = nameof(MyFavoriteClass);

        Console.WriteLine($"nameof({nameof(nameOfMyFavoriteClass)})      : {nameOfMyFavoriteClass}");
        Console.WriteLine($"typeof({nameof(nameOfMyFavoriteClass)}).Name : {typeOfMyFavoriteClass_Name}");
    }

    [TestMethod]
    public void NameOfGenericClass()
    {
        Console.WriteLine($"nameof(SomeGenericClass<int>):       {nameof(SomeGenericClass<int>)}");
        Console.WriteLine();

        Console.WriteLine($"typeof(SomeGenericClass<>).Name:     {typeof(SomeGenericClass<>).Name}");
        Console.WriteLine($"typeof(SomeGenericClass<>).FullName: {typeof(SomeGenericClass<>).FullName}");
        Console.WriteLine();

        Console.WriteLine($"typeof(SomeGenericClass<>).Name:     {typeof(SomeGenericClass<int>).Name}");
        Console.WriteLine($"typeof(SomeGenericClass<>).FullName: {typeof(SomeGenericClass<int>).FullName}");
    }
}

public class SomeClassWithAnAnnoyingName;

public class SomeGenericClass<T>;