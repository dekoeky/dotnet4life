using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace QuickTests.ThirdParty.CsvHelper.TypeConversion.IndexBaseExample;

internal sealed class IndexingBaseAwareInt32Converter : Int32Converter
{
    //Cached offsets for reading and writing
    private readonly int _readOffset;
    private readonly int _writeOffset;

    public IndexingBaseAwareInt32Converter(IndexBase file, IndexBase poco)
    {
        _readOffset = (file, poco) switch
        {
            (IndexBase.OneBased, IndexBase.ZeroBased) => -1,
            (IndexBase.ZeroBased, IndexBase.OneBased) => +1,
            _ => 0,
        };
        _writeOffset = -_readOffset;
    }


    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData mapData)
    {
        //Have the base class do the boilerplate for us
        var baseResult = base.ConvertFromString(text, row, mapData);

        return baseResult is int value  //We expect this to always be the case
            ? value + _readOffset           //Apply the cached offset
            : baseResult;
    }

    public override string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        if (value is int intValue)
            value = intValue + _writeOffset;

        return base.ConvertToString(value, row, memberMapData);
    }
}