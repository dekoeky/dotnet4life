using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace QuickTests.ThirdParty.CsvHelper;

internal static class Helpers
{
    public static string ToCsvStringSimple<T>(IEnumerable<T> records, CultureInfo? culture)
        where T : class, IPerson //Demonstrate only with IPerson Implementations
    {
        culture ??= CultureInfo.InvariantCulture;

        //Typically a filestream or other stream would be used here.
        //We use a StringWriter for demonstration purposes
        using var writer = new StringWriter();
        using var csv = new CsvWriter(writer, culture);

        csv.WriteRecords(records);

        return writer.ToString();
    }

    public static T[] FromCsvStringSimple<T>(string csvString, CultureInfo culture, string delimiter)
        where T : class, IPerson //Demonstrate only with IPerson Implementations
    {
        var config = new CsvConfiguration(culture)
        {
            //Good Practice to specify the delimiter (this is apart from the culture)
            Delimiter = delimiter
        };


        using var reader = new StringReader(csvString);
        using var csv = new CsvReader(reader, config);

        return csv.GetRecords<T>().ToArray();
    }
}