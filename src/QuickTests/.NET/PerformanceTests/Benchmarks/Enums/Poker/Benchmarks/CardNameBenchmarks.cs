using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace PerformanceTests.Benchmarks.Enums.Poker.Benchmarks;

[HtmlExporter]
[MarkdownExporterAttribute.Default]
[Orderer(SummaryOrderPolicy.Method)]
public abstract class CardNameBenchmarks
{
    public static IEnumerable<object[]> CardNameTestData() => Enum.GetValues<CardName>().Select(cardName => new object[] { cardName });
    public static IEnumerable<object[]> CardSuitTestData() => Enum.GetValues<CardSuit>().Select(cardSuit => new object[] { cardSuit });
}