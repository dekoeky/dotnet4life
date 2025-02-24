using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Collections;
using System.Reflection;
using System.Text;

namespace Formatters.Formatters.Output;

/// <summary>
/// 
/// </summary>
/// <seealso cref="SystemTextJsonOutputFormatter"/>
/// <seealso cref="StringOutputFormatter"/>
public class SimpleCsvOutputFormatter
    : TextOutputFormatter
        //: OutputFormatter
        , IOutputFormatter
        , IApiResponseTypeMetadataProvider
{
    private char splitChar = ';';

    public SimpleCsvOutputFormatter()
    {
        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(System.Net.Mime.MediaTypeNames.Text.Csv));
    }

    public override bool CanWriteResult(OutputFormatterCanWriteContext context)
    {
        return base.CanWriteResult(context);
    }

    protected override bool CanWriteType(Type? type)
    {
        return base.CanWriteType(type);
    }

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        if (context.Object is not IEnumerable items) throw new InvalidOperationException();

        var objectType = context.Object?.GetType() ?? context.ObjectType;
        var iEnumerableType = objectType?.GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        var elementType = iEnumerableType
                              ?.GetGenericArguments().FirstOrDefault()
                          ?? throw new InvalidOperationException();










        // Cast the object to IEnumerable<T>
        //var items = (IEnumerable?)Convert.ChangeType(context.Object, enumerableType);
        //var items = (IEnumerable)context.Object;
        var response = context.HttpContext.Response;

        await using var sw = new StreamWriter(response.Body, selectedEncoding);

        var props = elementType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

        var writeSplitter = false;

        //Write headers
        foreach (var p in props)
        {
            await sw.WriteAsync(writeSplitter ? splitChar + p.Name : p.Name);
            writeSplitter = true;
        }
        await sw.WriteLineAsync();
        writeSplitter = false;

        //Write values
        foreach (var item in items)
        {
            //Don't write empty rows
            if (item is null) continue;

            foreach (var p in props)
            {
                var value = p.GetValue(item);

                //Quick & dirty:
                await sw.WriteAsync(writeSplitter ? $"{splitChar}\"{value}\"" : $"\"{value}\"");
                writeSplitter = true;
            }

            await sw.WriteLineAsync();
            writeSplitter = false;
        }
    }
}