using System.Globalization;

namespace QuickTests.ThirdParty.CsvHelper.Basics;

[TestClass]
public class CsvHelperBasicTests
{
    [TestMethod]
    [DataRow("")] // InvariantCulture
    [DataRow("en-US")]
    [DataRow("nl-BE")]
    public void ToCsvStringSimple(string cultureName)
    {
        //ARRANGE
        var records = PersonTestData.Create<Person>();
        var culture = new CultureInfo(cultureName);

        //ACT
        var csvString = Helpers.ToCsvStringSimple(records, culture);

        //ASSERT
        Console.WriteLine(csvString);
    }

    [TestMethod]
    [DataRow("", PersonTestData.Invariant, ",")] // InvariantCulture
    [DataRow("en-US", PersonTestData.EnUs, ",")]
    [DataRow("nl-BE", PersonTestData.NlBe, ";")]
    public void FromCsvStringSimple(string cultureName, string csvString, string delimiter)
    {
        //ARRANGE
        var culture = new CultureInfo(cultureName);

        //ACT
        var records = Helpers.FromCsvStringSimple<Person>(csvString, culture, delimiter);

        //ASSERT
        Console.WriteLine(csvString);
        Assert.That.IsExpectedTestData(records);
    }
}