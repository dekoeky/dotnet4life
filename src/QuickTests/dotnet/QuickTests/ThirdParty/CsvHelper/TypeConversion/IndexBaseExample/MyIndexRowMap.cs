using CsvHelper.Configuration;

namespace QuickTests.ThirdParty.CsvHelper.TypeConversion.IndexBaseExample;

internal sealed class MyIndexRowMap : ClassMap<MyIndexRow>
{
    public MyIndexRowMap(IndexBase file, IndexBase poco)
    {
        Map(m => m.Index).TypeConverter(new IndexingBaseAwareInt32Converter(file: file, poco: poco));
    }
}