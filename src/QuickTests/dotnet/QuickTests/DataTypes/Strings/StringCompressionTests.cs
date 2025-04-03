using System.IO.Compression;
using System.Text;

namespace QuickTests.DataTypes.Strings;

[TestClass]
public class StringCompressionTests
{
    private static readonly string[] LoremIpsumSentences =
    [
        "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
        "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
        "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
        "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
        "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
    ];

    [DataTestMethod]
    [DynamicData(nameof(GetTestStrings))]
    public void CompressAndDecompressString(Func<string> retrieveString)
    {
        //Arrange
        var text = retrieveString();
        var encoding = Encoding.UTF8;

        //Act
        var compressed = Compress(text);
        var encoded = encoding.GetBytes(text);
        var length = encoded.Length;
        var compressedLength = compressed.Length;
        var compression = compressedLength / (double)length;


        var decompressed = DeCompress(compressed);


        //Assert
        Console.WriteLine($"Text Length:        {text.Length}");
        Console.WriteLine($"Encoded Length:     {length}");
        Console.WriteLine($"Compressed Length:  {compressedLength}");
        Console.WriteLine($"Compression:        {compression:P}");
        Assert.AreEqual(text, decompressed);
    }

    private static byte[] Compress(string str)
    {
        using var ms = new MemoryStream();
        using var gz = new GZipStream(ms, CompressionMode.Compress);
        using (var sw = new StreamWriter(gz))
            sw.Write(str);
        return ms.ToArray();
    }
    private static string DeCompress(byte[] bytes)
    {
        using var ms = new MemoryStream(bytes, false);
        using var gz = new GZipStream(ms, CompressionMode.Decompress);
        using var sw = new StreamReader(gz);
        return sw.ReadToEnd();
    }

    public static IEnumerable<Func<string>> GetTestStrings()
    {
        yield return () => "Hello World";
        yield return () => string.Join(Environment.NewLine, LoremIpsumSentences);
        yield return () => string.Join(Environment.NewLine, Enumerable.Repeat(string.Join(Environment.NewLine, LoremIpsumSentences), 10).ToArray());
        yield return () => string.Join(Environment.NewLine, Enumerable.Repeat(string.Join(Environment.NewLine, LoremIpsumSentences), 100).ToArray());
        yield return () => string.Join(Environment.NewLine, Enumerable.Repeat(string.Join(Environment.NewLine, LoremIpsumSentences), 1000).ToArray());
    }
}