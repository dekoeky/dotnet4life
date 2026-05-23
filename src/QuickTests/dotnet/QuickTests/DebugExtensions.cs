using System.Diagnostics;

namespace QuickTests;

public static class DebugExtensions
{
    extension(Debug)
    {
        public static void WriteLine() => Debug.WriteLine(null);
    }
}