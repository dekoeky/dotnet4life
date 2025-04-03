using System.Text.Json;

namespace QuickTests.OperatingSystem;

[TestClass]
public class OperatingSystemTests
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        WriteIndented = true,
    };

    [TestMethod]
    public void PrintCurrentOsDetails()
    {
        //ARRANGE
        var info = new
        {
            OS = Environment.OSVersion.Platform.ToString(),
            Environment.OSVersion.ServicePack,
            Environment.OSVersion.VersionString,
            OperatingSystemBits = Environment.Is64BitOperatingSystem ? "64-Bit" : "32-Bit",
        };

        //ACT
        var json = JsonSerializer.Serialize(info, JsonSerializerOptions);

        //ASSERT
        Console.WriteLine(json);
    }
}