namespace QuickTests.ThirdParty.CsvHelper.TypeConversion.IndexBaseExample;

[AttributeUsage(AttributeTargets.Property)]
internal sealed class BaseConversionAttribute : Attribute
{
    public IndexBase File { get; init; }
    public IndexBase Poco { get; init; }
}