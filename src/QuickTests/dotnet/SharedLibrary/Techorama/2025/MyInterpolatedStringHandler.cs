using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace SharedLibrary.Techorama._2025;

[InterpolatedStringHandler]
public readonly ref struct MyInterpolatedStringHandler
{
    private readonly StringBuilder _builder;

    public MyInterpolatedStringHandler(int literalLength, int formattedCount)
    {
        Debug.WriteLine($"Creating {nameof(MyInterpolatedStringHandler)}");
        _builder = new StringBuilder(literalLength);
    }

    public void AppendLiteral(string s)
    {
        Debug.WriteLine($"Appending string to {nameof(MyInterpolatedStringHandler)}");
        _builder.Append(s);
    }

    public void AppendFormatted<T>(T value)
    {
        Debug.WriteLine($"Appending formatted to {nameof(MyInterpolatedStringHandler)}");

        switch (value)
        {
            case short:
            case ushort:
            case int:
            case uint:
            case long:
            case ulong:
            case float:
            case double:
                _builder
                    .Append(">>>>>")
                    .Append(value)
                    .Append("<<<<<");
                return;

            default:
                _builder.Append(value);
                return;
        }
    }

    public override string ToString()
    {
        Debug.WriteLine($"Creating string from {nameof(MyInterpolatedStringHandler)}");
        return _builder.ToString();
    }
}