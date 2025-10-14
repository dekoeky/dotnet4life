namespace QuickTests.OperatingSystem;

[TestClass]
public class OSConditionAttributeTests
{
    [OSCondition(ConditionMode.Include, OperatingSystems.Windows)]
    [TestMethod("This test method should only execute on Windows")]
    public void HelloWorld_Windows() =>
        Console.WriteLine("Hello World, From Windows");

    [OSCondition(ConditionMode.Include, OperatingSystems.Linux)]
    [TestMethod("This test method should only execute on Linux")]
    public void HelloWorld_Linux() =>
        Console.WriteLine("Hello World, From Linux");
}