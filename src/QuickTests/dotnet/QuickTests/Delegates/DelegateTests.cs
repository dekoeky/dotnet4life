namespace QuickTests.Delegates;

/// <summary>
/// Tests/Demonstrations regarding <see href="https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/">Delegates</see>
/// </summary>
[TestClass]
public class DelegateTests
{
    /// <summary>
    /// A parameterless delegate for demonstration purposes.
    /// </summary>
    private delegate void MyDelegate();

    /// <summary>
    /// Demonstrates the usage of Multicast Delegates.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/how-to-combine-delegates-multicast-delegates"/>
    [TestMethod]
    public void MulticastDelegatesTest()
    {
        //ARRANGE
        var myStuff = new MyClassWithMethods("instance 1");
        MyDelegate myDelegate = MethodA;
        myDelegate += MethodB;
        myDelegate += MethodC;
        myDelegate += MethodX;
        myDelegate += () => Console.WriteLine("Lambda Delegate Executed");
        myDelegate += myStuff.Foo;
        myDelegate += myStuff.Bar;

        //ACT
        myDelegate.Invoke();
    }

    /// <summary>
    /// Demonstrates the usage of Multicast Delegates.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/how-to-combine-delegates-multicast-delegates"/>
    [TestMethod]
    public void EnumerateInvocationListTest()
    {
        // ARRANGE
        var myStuff = new MyClassWithMethods("instance x");

        // ACT
        MyDelegate fooAndBar = (MyDelegate)myStuff.Foo +
                               (MyDelegate)myStuff.Bar;

        MyDelegate myDelegate = MethodA;
        myDelegate += MethodB;
        myDelegate += MethodC;
        myDelegate += () => Console.WriteLine("Lambda Delegate Executed");
        myDelegate += myStuff.Foo;
        myDelegate += myStuff.Bar;
        myDelegate += fooAndBar;

        // ASSERT
        foreach (var myDel in Delegate.EnumerateInvocationList(myDelegate))
        {
            // Print the info on every delegate in the invocation list
            Console.WriteLine($"{(myDel.HasSingleTarget ? "Single Target" : "Multiple Targets")}");
            Console.WriteLine($"Method: {myDel.Method}");
            Console.WriteLine($"Target: {myDel.Target}");
            Console.WriteLine();
        }
    }

    // Some (static) test methods

    private static void MethodA() => Console.WriteLine($"{nameof(MethodA)} Executed");
    private static void MethodB() => Console.WriteLine($"{nameof(MethodB)} Executed");
    private static void MethodC() => Console.WriteLine($"{nameof(MethodC)} Executed");

    // Some (instance) test methods

    private void MethodX() => Console.WriteLine($"{nameof(MethodX)} executed");


    /// <summary>
    /// A class with some test methods for our delegate, to show delegates on instances of classes.
    /// </summary>
    /// <param name="instanceName"></param>
    private class MyClassWithMethods(string instanceName)
    {
        public void Foo() => Console.WriteLine($"{nameof(Foo)} on instance {instanceName} executed");
        public void Bar() => Console.WriteLine($"{nameof(Bar)} on instance {instanceName} executed");
    }
}
