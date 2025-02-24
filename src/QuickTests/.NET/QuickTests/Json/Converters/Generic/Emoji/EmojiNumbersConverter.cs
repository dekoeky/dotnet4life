using System.Globalization;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace QuickTests.Json.Converters.Generic.Emoji;

public class EmojiNumbersConverter<T> : JsonConverter<T> where T : INumber<T>
{
    private readonly StringBuilder _sb = new();
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        //Attempt to read the string value
        var s = reader.GetString() ?? throw new InvalidOperationException("Can't read string :(");

        _sb.Clear();

        foreach (var c in s)
        {
            switch (c)
            {
                //these two symbols follow the 0 to 9 symbol in our emojis
                case '\ufe0f':
                case '\u20e3':
                    continue;
                case '\u2796':
                    _sb.Append('-'); break;
                case '\u23fa':
                    _sb.Append('.'); break;
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    _sb.Append(c); break;

            }
        }

        //parse the resulting value as int
        return T.Parse(_sb.ToString(), CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        var s = value.ToString() ?? throw new InvalidOperationException();

        var emojis = s.Select(c => c switch
        {
            '0' => "0\ufe0f\u20e3",
            '1' => "1\ufe0f\u20e3",
            '2' => "2\ufe0f\u20e3",
            '3' => "3\ufe0f\u20e3",
            '4' => "4\ufe0f\u20e3",
            '5' => "5\ufe0f\u20e3",
            '6' => "6\ufe0f\u20e3",
            '7' => "7\ufe0f\u20e3",
            '8' => "8\ufe0f\u20e3",
            '9' => "9\ufe0f\u20e3",
            '-' => "\u2796",
            '.' or ',' => "\u23fa",

            _ => throw new InvalidOperationException(),
        });

        var complete = string.Join("", emojis);

        writer.WriteStringValue(complete);
    }
}