namespace Shared.Tests;

[TestClass]
public sealed class Test1
{
    [TestMethod]
    public void TestMethod1()
    {
        var type = typeof(ConsoleColor).GetEnumUnderlyingType();

        Console.WriteLine(type);
    }
}
