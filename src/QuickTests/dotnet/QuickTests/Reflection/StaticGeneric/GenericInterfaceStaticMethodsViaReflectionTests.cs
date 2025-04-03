using QuickTests.Reflection.StaticGeneric.Models;
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
    [DataRow(typeof(A))]
    [DataRow(typeof(B))]
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
    [DataRow(typeof(A))]
    [DataRow(typeof(B))]
    public void PrintImplementation(Type type)
    {
        //Arrange
        using var w = new IndentedTextWriter(Console.Out);
        var interfaceType = typeof(IDemo<,>);

        //Act
        var interfaceOnType = type
            .GetInterfaces()
            .FirstOrDefault(it => it.GetGenericTypeDefinition() == interfaceType);

        //Assert
        Assert.IsNotNull(interfaceOnType);
        w.WriteLine(
            $"{type.Name} implements {interfaceType.GetFriendlyTypeName()} as {interfaceOnType.GetFriendlyTypeName()}");
    }

    [DataTestMethod]
    [DataRow(typeof(A))]
    [DataRow(typeof(B))]
    public void CallInterfaceMethodOnImplementingClass(Type type)
    {
        //ARRANGE
        const double value = 5;
        const double expectedResult = 10;
        using var w = new IndentedTextWriter(Console.Out);
        var interfaceType = typeof(IDemo<,>);

        //ACT

        //Find the implemented interface
        var interfaceOnType = type
            .GetInterfaces()
            .FirstOrDefault(it => it.GetGenericTypeDefinition() == interfaceType)
            ?? throw new InvalidOperationException("We expect for this test that this is never null");

        //Get the interface mapping for the current type
        var map = type.GetInterfaceMap(interfaceOnType);

        //Grab the interface method, assuming there is only one for now
        var targetMethod = map.TargetMethods.First();

        //Convert it into a delegate
        var timesTwo = targetMethod.CreateDelegate<TimesTwoDelegate>();

        //Call the delegate
        var result = timesTwo(value);

        //ASSERT
        Assert.AreEqual(expectedResult, result);
    }
}