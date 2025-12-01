namespace SharedLibrary.AsyncEnumerable;

internal record ProcessingData
{
    public DatabaseData DatabaseData { get; set; }

    public DateTime ProcessingStart { get; set; }

    public DateTime ProcessingEnd { get; set; }
}