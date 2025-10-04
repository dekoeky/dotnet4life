using SharedLibrary.Techorama._2025;

namespace QuickTests.Techorama._2025;

[TestClass]
public class CustomInterpolatedStringHandler
{
    [TestMethod]
    public void LogInfoString()
    {
        //ARRANGE
        const int favoriteNumber = 15;

        //ACT
        LogInfoString($"My Favorite Number: {favoriteNumber}");

        //ASSERT

    }

    [TestMethod]
    public void LogInfoMyInterpolatedStringHandler()
    {
        //ARRANGE
        const int favoriteNumber = 15;

        //ACT
        LogInfoMyInterpolatedStringHandler($"My Favorite Number: {favoriteNumber}");

        //ASSERT

    }

    private static void LogInfoMyInterpolatedStringHandler(MyInterpolatedStringHandler handler)
    {
        Console.WriteLine(handler.ToString());
    }

    private static void LogInfoString(string message)
    {
        Console.WriteLine(message);
    }
}
