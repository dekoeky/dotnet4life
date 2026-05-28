namespace QuickTests.Generics;

[TestClass]
public class StaticFieldsInGenericClassesTests
{
    [TestMethod]
    public void TestMethod1()
    {
        //ARRANGE
        Assert.AreEqual(0, MyStaticClass1<short>.Counter);
        Assert.AreEqual(0, MyStaticClass1<long>.Counter);

        //ACT
        MyStaticClass1<short>.Counter = 5;
        MyStaticClass1<long>.Counter = 10;

        //ASSERT
        Assert.AreEqual(5, MyStaticClass1<short>.Counter);
        Assert.AreEqual(10, MyStaticClass1<long>.Counter);
    }

    [TestMethod]
    public void TestMethod2()
    {
        //ARRANGE
        Assert.AreEqual(0, MyStaticClass2<short>.Counter);
        Assert.AreEqual(0, MyStaticClass2<long>.Counter);

        //ACT
        MyStaticClass2<short>.Counter = 5;
        MyStaticClass2<long>.Counter = 10;

        //ASSERT
        Assert.AreEqual(10, MyStaticClass2<short>.Counter);
        Assert.AreEqual(10, MyStaticClass2<long>.Counter);
    }
}
file static class MyStaticClass1<T>
{
    // ReSharper Warning StaticMemberInGenericType
    // https://www.jetbrains.com/help/resharper/StaticMemberInGenericType.html
    public static int Counter { get; set; }
}


file class MyStaticClass2<T> : MyStaticClass2;

file class MyStaticClass2
{
    public static int Counter { get; set; }
}
