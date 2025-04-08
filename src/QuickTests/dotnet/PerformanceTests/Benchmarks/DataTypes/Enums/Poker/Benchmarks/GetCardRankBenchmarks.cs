using BenchmarkDotNet.Attributes;
using PerformanceTests.Benchmarks.DataTypes.Enums.Poker;

namespace PerformanceTests.Benchmarks.DataTypes.Enums.Poker.Benchmarks;

public class GetCardRankBenchmarks : CardNameBenchmarks
{
    [Benchmark]
    [ArgumentsSource(nameof(CardNameTestData))]
    public CardRank GetCardRank(CardName cardName) => CardMath.GetRank(cardName);
}