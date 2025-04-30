using SharedLibrary;
using SharedLibrary.Reflection.StaticGeneric.Models;
using System.CodeDom.Compiler;
using InvalidOperationException = System.InvalidOperationException;

namespace QuickTests.Reflection.StaticGeneric;

/// <summary>
/// Demonstrates calling static methods, on generic interfaces, on their implementing types (via reflection).
/// </summary>
[TestClass]
public class GenericInterfaceStaticMethodsViaReflectionTests
{
    [DataTestMethod]
    [DataRow(typeof(TimesTwo))]
    [DataRow(typeof(TimesThree))]
    public void PrintReflectionInfo(Type type)
    {
        //Arrange
        using var w = new IndentedTextWriter(Console.Out);

        //Act
        w.WriteLine($"Type {type.Name} has the following:");
        w.Indent++;
        w.WriteHeaderWithItems("Methods:", type.GetMethods().Select(m => $"{m.Name}"));
        w.WriteHeaderWithItems("Interfaces:", type.GetInterfaces().Select(i => $"{i.GetFriendlyTypeName()}"));
    }

    [DataTestMethod]
    [DataRow(typeof(TimesTwo))]
    [DataRow(typeof(TimesThree))]
    public void PrintImplementation(Type type)
    {
        //Arrange
        using var writer = new IndentedTextWriter(Console.Out);
        var interfaceType = typeof(IDemo<,>);

        //Act
        var interfaceOnType = type
            .GetInterfaces()
            .FirstOrDefault(it => it.GetGenericTypeDefinition() == interfaceType);

        //Assert
        Assert.IsNotNull(interfaceOnType);
        writer.WriteLine(
            $"{type.Name} implements {interfaceType.GetFriendlyTypeName()} " +
            $"as {interfaceOnType.GetFriendlyTypeName()}");
    }

    /// <summary>
    /// This test attempts to retrieve the <see cref="IDemo{TSelf,TOther}"/> interface on the given type,
    /// and call its static method <see cref="IDemo{TSelf,TOther}.Execute(double)"/> via reflection.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    /// <param name="expectedResult"></param>
    /// <exception cref="InvalidOperationException"></exception>
    [DataTestMethod]
    [DataRow(typeof(TimesTwo), 5, 10)]
    [DataRow(typeof(TimesThree), 5, 15)]
    public void CallInterfaceMethodOnImplementingClass(Type type, double value, double expectedResult)
    {
        //ARRANGE
        var interfaceType = typeof(IDemo<,>);

        //ACT

        //Find the implemented interface
        var interfaceOnType = type
            .GetInterfaces()
            .SingleOrDefault(it => it.GetGenericTypeDefinition() == interfaceType)
            ?? throw new InvalidOperationException("We expect for this test that this is never null");

        //Get the interface mapping for the current type
        var map = type.GetInterfaceMap(interfaceOnType);

        //Grab the interface method, assuming there is only one for now
        var targetMethod = map.TargetMethods.First();

        //Convert it into a delegate
        var @delegate = targetMethod.CreateDelegate<DoubleOperationDelegate>();

        //Call the delegate
        var result = @delegate.Invoke(value);

        //ASSERT
        Assert.AreEqual(expectedResult, result);
    }
}