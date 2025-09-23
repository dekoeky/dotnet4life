namespace QuickTests.ThirdParty.CsvHelper.TypeConversion.IndexBaseExample;

public record MyIndexRow //record -> for equality check
{
    [BaseConversion(File = IndexBase.ZeroBased, Poco = IndexBase.OneBased)]
    public int Index { get; init; }
}