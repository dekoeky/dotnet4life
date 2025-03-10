namespace SharedLibrary.DataTypes.Formatting;

public class ExampleFormattable : ISpanFormattable
{
    private readonly DateTime _created = DateTime.Now;


    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        var now = DateTime.Now;
        FormattableString formattable = $"{nameof(_created)}: {_created}, {nameof(now)}: {now}";
        return formattable.ToString(formatProvider);
    }

    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format,
        IFormatProvider? provider)
    {
        var now = DateTime.Now;
        return destination.TryWrite(provider, $"{nameof(_created)}: {_created}, {nameof(now)}: {now}",
            out charsWritten);
    }

    public override string ToString()
    {
        var now = DateTime.Now;
        return $"{nameof(_created)}: {_created}, {nameof(now)}: {now}";
    }
}