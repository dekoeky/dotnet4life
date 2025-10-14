using System.Runtime.InteropServices;

namespace QuickTests.OperatingSystem.TestData;

public static class OSPlatformTestData
{

    public static IEnumerable<OSPlatform> AllPlatforms()
    {
        yield return OSPlatform.Linux;
        yield return OSPlatform.FreeBSD;
        yield return OSPlatform.OSX;
        yield return OSPlatform.Windows;
    }
}