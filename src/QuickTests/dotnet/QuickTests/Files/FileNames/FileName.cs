namespace QuickTests.Files.FileNames;

[TestClass]
public class FileName
{
    [TestMethod]
    public void ShowTestPaths()
    {
        Console.WriteLine($"Environment.CurrentDirectory: {Environment.CurrentDirectory}");
        //Console.WriteLine($"Path.GetFullPath(\"\"): {Path.GetFullPath("")}"); // ERROR
        Console.WriteLine($"Path.GetFullPath(\".\"): {Path.GetFullPath(".")}");
        Console.WriteLine($"Path.GetFullPath(\"TestDir\"): {Path.GetFullPath("TestDir")}");
        Console.WriteLine($"Path.Combine(Environment.CurrentDirectory, \"TestDir\"): {Path.Combine(Environment.CurrentDirectory, "TestDir")}");
    }
}