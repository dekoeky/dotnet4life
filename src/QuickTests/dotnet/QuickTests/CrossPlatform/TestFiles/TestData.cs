namespace QuickTests.CrossPlatform.TestFiles;

public static class TestData
{
    //Notes on paths below:
    // - The paths are relative to the project root (where the .csproj file is).
    // - The paths use forward slashes, as that works on all platforms.
    // - The files are set to "Copy if newer" in their properties, so they end up in the output directory.

    public const string CrLfFilePath = "CrossPlatform/TestFiles/HelloWorld.crlf.txt";
    public const string LfFilePath = "CrossPlatform/TestFiles/HelloWorld.lf.txt";

    public const string CrLfFileContent = "Hello\r\nWorld\r\n";
    public const string LfFileContent = "Hello\nWorld\n";

    public static readonly string[] ExpectedLines = [
        "Hello",
        "World",
        // Note: The empty line at the file end is not expected to shop up in the resulting lines.
    ];
}