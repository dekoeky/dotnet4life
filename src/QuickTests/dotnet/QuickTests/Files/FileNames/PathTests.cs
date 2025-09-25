using QuickTests._Helpers;

namespace QuickTests.Files.FileNames;

/// <summary>
/// <see cref="Path"/> related tests.
/// </summary>
[TestClass]
public class PathTests
{
    [RepeatTestMethod(5)]
    public void GetTempFileName()
    {
        // ---------- ACT --------------
        var path = Path.GetTempFileName();

        // ---------- ASSERT -----------
        Console.WriteLine(path);
    }

    [TestMethod]
    public void GetTempPath()
    {
        // ---------- ACT --------------
        var path = Path.GetTempPath();

        // ---------- ASSERT -----------
        Console.WriteLine(path);
    }
}