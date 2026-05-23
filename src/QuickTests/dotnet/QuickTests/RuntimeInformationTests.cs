using System.Runtime.InteropServices;

namespace QuickTests;

/// <summary>
/// <see cref="RuntimeInformation"/> related tests.
/// </summary>
[TestClass]
public class RuntimeInformationTests
{
    [TestMethod]
    public void RuntimeInformations()
    {
        Console.WriteLine($"RuntimeInformation.FrameworkDescription: {RuntimeInformation.FrameworkDescription}");
        Console.WriteLine($"RuntimeInformation.OSArchitecture:       {RuntimeInformation.OSArchitecture}");
        Console.WriteLine($"RuntimeInformation.OSDescription:        {RuntimeInformation.OSDescription}");
        Console.WriteLine($"RuntimeInformation.ProcessArchitecture:  {RuntimeInformation.ProcessArchitecture}");
        Console.WriteLine($"RuntimeInformation.RuntimeIdentifier:    {RuntimeInformation.RuntimeIdentifier}");
    }

    [TestMethod]
    [DataRow("WINDOWS")]
    [DataRow("LINUX")]
    [DataRow("OSX")]
    [DataRow("FREEBSD")]
    public void IsOSPlatform(string platformName)
    {
        // Arrange
        var platform = OSPlatform.Create(platformName);

        // Act
        var result = RuntimeInformation.IsOSPlatform(platform);

        // Assert
        Console.WriteLine($"{nameof(RuntimeInformation)}.{nameof(RuntimeInformation.IsOSPlatform)}({platformName,10}) => {result}");
    }
}