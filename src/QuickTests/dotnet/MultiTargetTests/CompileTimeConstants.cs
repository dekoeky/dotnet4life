using System.Diagnostics.CodeAnalysis;

namespace MultiTargetTests;

[SuppressMessage("ReSharper", "InconsistentNaming")]
internal static class CompileTimeConstants
{
#if NET47
    public const string TFM = "net47";
#elif NET471
    public const string TFM = "net471";
#elif NET472
    public const string TFM = "net472";
#elif NET48
    public const string TFM = "net48";
#elif NET481
    public const string TFM = "net481";
#elif NET8_0
    public const string TFM = "net8.0";
#elif NET9_0
    public const string TFM = "net9.0";
#else
#error Could not detect TFM (Not Specified in CompileTimeConstants)
#endif
}