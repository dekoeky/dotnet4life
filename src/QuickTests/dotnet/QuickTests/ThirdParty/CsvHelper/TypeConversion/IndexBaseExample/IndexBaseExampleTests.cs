using CsvHelper;
using System.Diagnostics;
using System.Globalization;

namespace QuickTests.ThirdParty.CsvHelper.TypeConversion.IndexBaseExample;

[TestClass]
public class IndexBaseExampleTests
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

    [TestMethod]
    [MyIndexRowTestData]
    public void Read(string csv, MyIndexRow[] expected, IndexBase file, IndexBase poco)
    {
        // ---------- ARRANGE ----------
        using var reader = new StringReader(csv);
        using var csvReader = new CsvReader(reader, Culture);

        //Where the magic happens:
        csvReader.Context.RegisterClassMap(new MyIndexRowMap(file: file, poco: poco));

        // ---------- ACT --------------
        var actual = csvReader.GetRecords<MyIndexRow>().ToList();
        Debug.WriteLine($"Raw Csv Content ({file}):{Environment.NewLine}" +
                        $"{csv}{Environment.NewLine}");
        Debug.WriteLine($"Actual Result ({poco}):{Environment.NewLine}" +
                        $"{string.Join(Environment.NewLine, actual.Select(r => r.Index))}");

        // ---------- ASSERT -----------
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    [MyIndexRowTestData]
    public void Write(string expectedCsv, MyIndexRow[] data, IndexBase file, IndexBase poco)
    {
        // ---------- ARRANGE ----------
        using var writer = new StringWriter();
        using var csvWriter = new CsvWriter(writer, Culture);

        //Where the magic happens:
        csvWriter.Context.RegisterClassMap(new MyIndexRowMap(file: file, poco: poco));

        // ---------- ACT --------------
        csvWriter.WriteRecords(data);
        var actualCsv = writer.ToString();

        Debug.WriteLine($"Raw Data ({poco}):{Environment.NewLine}" +
                        $"{string.Join(Environment.NewLine, data.Select(r => r.Index))}");
        Debug.WriteLine($"Actual Csv Content ({file}):{Environment.NewLine}" +
                        $"{actualCsv}{Environment.NewLine}");

        // ---------- ASSERT -----------
        //Cleanup data before comparison
        expectedCsv = expectedCsv.Trim().ReplaceLineEndings();
        actualCsv = actualCsv.Trim().ReplaceLineEndings();
        Assert.AreEqual(expectedCsv, actualCsv); //Usually not a good idea, however, for this test we tailored our testdata
    }
}