using System.CodeDom.Compiler;
using System.Reflection;

namespace QuickTests.Reflection;

/// <summary>
/// Demonstrates calling static interface methods, on implementing types (via reflection).
/// </summary>
[TestClass]
public class StaticInterfaceMethodsViaReflection
{
    [TestMethod]
    public void MethodC_StaticMethodOnType()
    {
        //Arrange
        var type = typeof(TestType);
        var method = type.GetMethod(nameof(TestType.MethodC),
                         BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                     ?? throw new InvalidOperationException("Could not get method");

        //Act
        method.Invoke(null, null);

        //Assert
        Assert.IsTrue(TestType.Called);
    }

    [TestMethod]
    public void MethodA_ViaImplicitInterfaceImplementation()
    {
        //Arrange
        var type = typeof(TestType);
        var method = type.GetMethod(nameof(TestType.MethodA),
                         BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                     ?? throw new InvalidOperationException("Could not get method");

        //Act
        method.Invoke(null, null);

        //Assert
        Assert.IsTrue(TestType.Called);
    }

    [TestMethod]
    public void MethodA_ViaExplicitInterfaceImplementation()
    {
        //Arrange
        var type = typeof(TestType);
        var interfaceMapping = type.GetInterfaceMap(typeof(IA));
        var targetMethod = interfaceMapping.TargetMethods.First();

        //Act
        targetMethod.Invoke(null, null);

        //Assert
        Assert.IsTrue(TestType.Called);
    }

    [TestMethod]
    public void MethodB_ViaExplicitInterfaceImplementation()
    {
        //Arrange
        var type = typeof(TestType);
        var interfaceMapping = type.GetInterfaceMap(typeof(IB));
        var targetMethod = interfaceMapping.TargetMethods.First();

        //Act
        targetMethod.Invoke(null, null);

        //Assert
        Assert.IsTrue(TestType.Called);
    }

    [TestMethod]
    public void PrintReflectionInfo()
    {
        //Arrange
        using var w = new IndentedTextWriter(Console.Out);
        var type = typeof(TestType);

        //Act
        w.WriteLine($"Type {type.Name} has the following:");
        w.Indent++;
        w.WriteHeaderWithItems("Methods:", type.GetMethods().Select(m => $"{m.Name}"));
        w.WriteHeaderWithItems("Interfaces:", type.GetInterfaces().Select(i => $"{i.Name}"));
    }

    private class TestType : IA, IB
    {
        public static bool Called = false;


        public static void MethodA()
        {
            Console.WriteLine($"Calling {nameof(MethodA)}, which is implicitly implemented on type {nameof(TestType)}");
            Called = true;
        }

        static void IB.MethodB()
        {
            Console.WriteLine($"Calling {nameof(IB.MethodB)}, which is explicitly implemented on type {nameof(TestType)}");
            Called = true;
        }

        public static void MethodC()
        {
            Console.WriteLine($"Calling {nameof(MethodC)}, which is a static method on type {nameof(TestType)}");
            Called = true;
        }
    }

    private interface IA
    {
        static abstract void MethodA();
    }

    private interface IB
    {
        static abstract void MethodB();
    }
}