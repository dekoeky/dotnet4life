using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace Formatters.Formatters.Input;

/// <summary>
/// 
/// </summary>
/// <seealso cref="SystemTextJsonInputFormatter"/>
public class SimpleCsvInputFormatter
    : TextInputFormatter
        //: InputFormatter
        , IInputFormatter

{
    private char splitChar = ';';

    public SimpleCsvInputFormatter()
    {
        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(System.Net.Mime.MediaTypeNames.Text.Csv));
    }

    public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
    {
        throw new NotImplementedException();
    }
}