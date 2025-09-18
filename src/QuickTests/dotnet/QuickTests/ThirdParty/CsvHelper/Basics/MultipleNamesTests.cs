using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System.Globalization;

namespace QuickTests.ThirdParty.CsvHelper.Basics;


[TestClass]
public class MultipleNamesTests
{
    private const string Dash = """
                                Id,my-age,my-name
                                1,20,Batman
                                2,50,Spiderman
                                """;
    private const string Underscore = """
                                      Id,my_age,my_name
                                      1,20,Batman
                                      2,50,Spiderman
                                      """;
    private const string Plus = """
                                      Id,my+age,my+name
                                      1,20,Batman
                                      2,50,Spiderman
                                      """;
    private const string Hat = """
                                Id,my^age,my^name
                                1,20,Batman
                                2,50,Spiderman
                                """;

    private Poco[] _expected =
    [
        new () { Id = 1, MyAge = 20, MyName = "Batman", },
        new () { Id = 2, MyAge = 50, MyName = "Spiderman", },
    ];

    [TestMethod]
    [DataRow(Dash)]
    [DataRow(Underscore)]

    public void UsingAttributes(string csv)
    {
        // ---------- ARRANGE ----------
        using var sr = new StringReader(csv);
        using var cr = new CsvReader(sr, CultureInfo.InvariantCulture);

        // ---------- ACT --------------
        var records = cr.GetRecords<Poco>().ToArray();

        // ---------- ASSERT -----------
        CollectionAssert.AreEqual(_expected, records);
    }

    [TestMethod]
    [DataRow(Hat)]
    [DataRow(Plus)]

    public void UsingClassMap(string csv)
    {
        // ---------- ARRANGE ----------
        using var sr = new StringReader(csv);
        using var cr = new CsvReader(sr, CultureInfo.InvariantCulture);
        cr.Context.RegisterClassMap<PocoClassMap>();

        // ---------- ACT --------------
        var records = cr.GetRecords<Poco>().ToArray();

        // ---------- ASSERT -----------
        CollectionAssert.AreEqual(_expected, records);
    }

    private record Poco
    {
        public int Id { get; set; }

        [Name("my-name", "my_name")]
        public string MyName { get; init; }

        [Name("my-age", "my_age")]
        public int MyAge { get; init; }
    }

    private class PocoClassMap : ClassMap<Poco>
    {
        public PocoClassMap()
        {
            Map(m => m.Id).Name("Id");
            Map(m => m.MyName).Name("my+name", "my^name");
            Map(m => m.MyAge).Name("my+age", "my^age");
        }
    }
}