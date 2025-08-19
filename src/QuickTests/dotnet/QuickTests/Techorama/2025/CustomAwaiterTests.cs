using SharedLibrary.Techorama._2025;

namespace QuickTests.Techorama._2025;

/// <summary>
/// <see cref="CustomStringAwaiter"/> related tests.
/// </summary>
/// <seealso cref="CustomStringAwaiterExtensions"/>
/// <seealso cref="CustomStringAwaiter"/>
[TestClass]
public class CustomAwaiterTests
{
    [TestMethod]
    public async Task Await_A_String()
    {
        //ARRANGE
        string? myString = "Hello World!";
        var expected = TimeSpan.FromMilliseconds(myString?.Length * 100 ?? 0);

        //ACT
        var before = DateTime.Now;
        await myString;
        var after = DateTime.Now;
        var took = after - before;

        //ASSERT
        Console.WriteLine($"Before  : {before:O}");
        Console.WriteLine($"After   : {after:O}");
        Console.WriteLine($"Took    : {took:G}");
        Console.WriteLine($"Expected: {expected:G}");
        Assert.IsTrue(took >= expected);
    }
}