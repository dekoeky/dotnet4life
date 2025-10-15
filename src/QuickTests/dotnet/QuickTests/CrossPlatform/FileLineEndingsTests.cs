using QuickTests.CrossPlatform.TestFiles;

namespace QuickTests.CrossPlatform;

[TestClass]
public class FileLineEndingsTests
{
    [TestMethod]
    [DataRow(TestData.CrLfFilePath, TestData.CrLfFileContent)]
    [DataRow(TestData.LfFilePath, TestData.LfFileContent)]
    public void ValidateTestFileContents(string path, string expectedContent)
    {
        // ---------- ACT --------------
        var content = File.ReadAllText(path);

        // ---------- ASSERT -----------
        Assert.AreEqual(expectedContent, content, StringComparer.Ordinal);
    }

    [TestMethod]
    [DataRow(TestData.CrLfFilePath)]
    [DataRow(TestData.LfFilePath)]
    public void ReadAllLines(string path)
    {
        // ---------- ACT --------------
        var lines = File.ReadAllLines(path);

        // ---------- ASSERT -----------
        CollectionAssert.AreEqual(TestData.ExpectedLines, lines, StringComparer.Ordinal);
    }

    /// <summary>
    /// Demonstrates that both CRLF and LF line endings are correctly handled by <see cref="File.ReadLines(string)"/> on both linux and Windows.
    /// </summary>
    [TestMethod]
    [DataRow(TestData.CrLfFilePath)]
    [DataRow(TestData.LfFilePath)]
    public void ReadLines(string path)
    {
        // ---------- ACT --------------
        var lines = File.ReadAllLines(path);

        // ---------- ASSERT -----------
        CollectionAssert.AreEqual(TestData.ExpectedLines, lines, StringComparer.Ordinal);
    }
}