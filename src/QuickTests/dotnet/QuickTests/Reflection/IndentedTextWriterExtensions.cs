using System.CodeDom.Compiler;

namespace QuickTests.Reflection;

internal static class IndentedTextWriterExtensions
{
    public static void WriteHeaderWithItems(this IndentedTextWriter tw, string title, IEnumerable<string> s)
    {
        using var e = s.GetEnumerator();

        tw.Write(title);
        if (!e.MoveNext())
            return;
        tw.WriteLine();

        tw.Indent++;
        do
        {
            tw.WriteLine(e.Current);
        } while (e.MoveNext());

        tw.Indent--;
    }
}