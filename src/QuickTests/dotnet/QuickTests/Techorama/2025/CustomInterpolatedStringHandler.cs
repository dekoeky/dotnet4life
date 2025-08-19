using SharedLibrary.Techorama._2025;

namespace QuickTests.Techorama._2025;

[TestClass]
public class CustomInterpolatedStringHandler
{
    [TestMethod]
    public void LogInfoString()
    {
        //ARRANGE
        var favoriteNumber = 15;

        //ACT
        LogInfoString($"My Favorite Number: {favoriteNumber}");

        //ASSERT

    }

    [TestMethod]
    public void LogInfoMyInterpolatedStringHandler()
    {
        //ARRANGE
        var favoriteNumber = 15;

        //ACT
        LogInfoMyInterpolatedStringHandler($"My Favorite Number: {favoriteNumber}");

        //ASSERT

    }

    public static void LogInfoMyInterpolatedStringHandler(MyInterpolatedStringHandler handler)
    {
        Console.WriteLine(handler.ToString());
    }

    public static void LogInfoString(string message)
    {
        Console.WriteLine(message);
    }
}
