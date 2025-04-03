using BenchmarkDotNet.Attributes;

namespace PerformanceTests.Benchmarks.Enums.Poker.Benchmarks;

public class GetCardRankBenchmarks : CardNameBenchmarks
{
    [Benchmark]
    [ArgumentsSource(nameof(CardNameTestData))]
    public CardRank GetCardRank(CardName cardName) => CardMath.GetRank(cardName);
}