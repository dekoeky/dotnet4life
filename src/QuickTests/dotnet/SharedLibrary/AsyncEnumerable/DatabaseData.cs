namespace SharedLibrary.AsyncEnumerable;

internal record DatabaseData
{
    public int Number { get; set; }
    public DateTime DatabaseCallStart { get; set; }
    public DateTime DatabaseCallEnd { get; set; }
}