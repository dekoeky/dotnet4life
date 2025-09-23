using System.Reflection;

namespace QuickTests.ThirdParty.CsvHelper.TypeConversion.IndexBaseExample;

[AttributeUsage(AttributeTargets.Method)]
internal sealed class MyIndexRowTestDataAttribute : Attribute, ITestDataSource
{
    private const string CsvZeroBased = """
                                       Index
                                       0
                                       1
                                       2
                                       3
                                       4
                                       """;

    private const string CsvOneBased = """
                                      Index
                                      1
                                      2
                                      3
                                      4
                                      5
                                      """;

    private static readonly MyIndexRow[] PocoZeroBased = Enumerable.Range(0, 5)
        .Select(i => new MyIndexRow { Index = i })
        .ToArray();

    private static readonly MyIndexRow[] PocoOneBased = Enumerable.Range(1, 5)
        .Select(i => new MyIndexRow { Index = i })
        .ToArray();

    public IEnumerable<object?[]> GetData(MethodInfo methodInfo)
    {
        yield return [CsvZeroBased, PocoOneBased, IndexBase.ZeroBased, IndexBase.OneBased];
        yield return [CsvOneBased, PocoZeroBased, IndexBase.OneBased, IndexBase.ZeroBased];
        yield return [CsvZeroBased, PocoZeroBased, IndexBase.ZeroBased, IndexBase.ZeroBased];
        yield return [CsvOneBased, PocoOneBased, IndexBase.OneBased, IndexBase.OneBased];
    }

    public string? GetDisplayName(MethodInfo methodInfo, object?[]? data) =>
        data is not [string csv, MyIndexRow[] expected, IndexBase file, IndexBase poco]
            ? throw new InvalidOperationException()
            : $"{methodInfo.Name} (File: {file} / Data: {poco})";
}