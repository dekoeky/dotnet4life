namespace QuickTests.Files.FileNames;

[TestClass]
public class FileExtensionTests
{
    [TestMethod]
    [DynamicData(nameof(FilePaths))]
    [DynamicData(nameof(FileNames))]
    public void FileExtensionsDemo(string? path)
    {
        // ---------- ARRANGE ----------
        const string newExtension = "new-extension";

        // ---------- ACT --------------
        var hasExtension = Path.HasExtension(path);
        var extension = Path.GetExtension(path);
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
        var changedExtension = Path.ChangeExtension(path, newExtension);

        // ---------- ASSERT -----------
        Console.WriteLine($"Path:                           {path}");
        Console.WriteLine($"Has Extension:                  {hasExtension}");
        Console.WriteLine($"Extension:                      {extension}");
        Console.WriteLine($"File Name Without Extension:    {fileNameWithoutExtension}");
        Console.WriteLine($"Changed Extension:              {changedExtension}");
        Console.WriteLine($"(new extension: {newExtension})");
    }

    public static IEnumerable<string> FilePaths()
    {
        yield return @"c:\temp\my-Data.csv";
        yield return @"c:\temp\file-without-extension";
        yield return @"c:\temp\file-ends-with-dot.";
        yield return @"c:\temp\single-letter-extension.w";
    }

    public static IEnumerable<string> FileNames()
    {
        yield return "my-Data.csv";
        yield return "file-without-extension";
        yield return "file-ends-with-dot.";
        yield return "single-letter-extension.w";
    }

    [TestMethod]
    [DynamicData(nameof(DualExtensionFileNames))]
    public void DualExtensions(string? path)
    {
        // ---------- ARRANGE ----------
        const string newExtension = "new-extension";

        // ---------- ACT --------------
        var hasExtension = Path.HasExtension(path);
        var extension = Path.GetExtension(path);
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
        var changedExtension = Path.ChangeExtension(path, newExtension);

        // ---------- ASSERT -----------
        Console.WriteLine($"Path:                           {path}");
        Console.WriteLine($"Has Extension:                  {hasExtension}");
        Console.WriteLine($"Extension:                      {extension}");
        Console.WriteLine($"File Name Without Extension:    {fileNameWithoutExtension}");
        Console.WriteLine($"Changed Extension:              {changedExtension}");
        Console.WriteLine($"(new extension: {newExtension})");
    }

    public static IEnumerable<string> DualExtensionFileNames()
    {
        yield return "lambert72-coordinates-meters.l74a.m.csv";
        yield return "lambert72-coordinates-feet.l74a.ft.csv";
        yield return "lambert72-coordinates-millimeters.l74a.mm.csv";
        yield return "unity-coordinates.unity.v3.csv";
    }
}