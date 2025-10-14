namespace QuickTests.OperatingSystem;

/// <summary>
/// <see cref="OperatingSystem"/> related tests.
/// </summary>
[TestClass]
public class OperatingSystemTests
{
    [TestMethod("Print the Current Operating System Details")]
    public void PrintCurrentOsDetails()
    {
        //ACT
        var os = Environment.OSVersion;

        //ASSERT
        Console.WriteLine($"Platform:                           {os.Platform}");
        Console.WriteLine($"ServicePack:                        {os.ServicePack}");
        Console.WriteLine($"ServicePack:                        {os.Version}");
        Console.WriteLine($"ServicePack:                        {os.VersionString}");
        Console.WriteLine($"Environment.Is64BitOperatingSystem: {Environment.Is64BitOperatingSystem}");
    }

    [TestMethod]
    [DataRow("Browser")]
    [DataRow("Linux")]
    [DataRow("FreeBSD")]
    [DataRow("Android")]
    [DataRow("iOS")]
    [DataRow("macOS")]
    [DataRow("tvOS")]
    [DataRow("watchOS")]
    [DataRow("Windows")]
    public void IsOSPlatform(string platform)
    {
        //ACT
        var result = System.OperatingSystem.IsOSPlatform(platform);

        //ASSERT
        Console.WriteLine($"System.OperatingSystem.IsOSPlatform({platform}): {result}");
    }
}