using QuickTests.OperatingSystem.TestData;
using System.Runtime.InteropServices;

namespace QuickTests.OperatingSystem;

[TestClass]
public class RuntimeInformationTests
{
    [TestMethod]
    public void PrintRuntimeInformation()
    {
        Console.WriteLine($"FrameworkDescription: {RuntimeInformation.FrameworkDescription}");
        Console.WriteLine($"OSArchitecture:       {RuntimeInformation.OSArchitecture}");
        Console.WriteLine($"OSDescription:        {RuntimeInformation.OSDescription}");
        Console.WriteLine($"ProcessArchitecture:  {RuntimeInformation.ProcessArchitecture}");
        Console.WriteLine($"RuntimeIdentifier:    {RuntimeInformation.RuntimeIdentifier}");
    }

    /// <seealso cref="OperatingSystemTests.IsOSPlatform"/>
    [TestMethod]
    [DynamicData(nameof(OSPlatformTestData.AllPlatforms), typeof(OSPlatformTestData))]
    public void IsOSPlatform(OSPlatform platform)
    {
        // ---------- ACT --------------
        var result = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        // ---------- ASSERT -----------
        Console.WriteLine($"RuntimeInformation.IsOSPlatform({platform}): {result}");
    }
}