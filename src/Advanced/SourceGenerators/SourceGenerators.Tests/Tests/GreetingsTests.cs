using This.Was.SourceGenerated;

namespace SourceGenerators.Tests;

[TestClass]
public class GreetingsTests
{
    [TestMethod]
    public void HelloFrom()
    {
        // ---------- ACT --------------
        Greetings.HelloFrom("Unit Test");
    }

    [TestMethod]
    public void GeneratedAtUtc()
    {
        // ---------- ACT --------------
        Console.WriteLine($"{nameof(Greetings.GeneratedAtUtc)}: {Greetings.GeneratedAtUtc:O}");
    }
}