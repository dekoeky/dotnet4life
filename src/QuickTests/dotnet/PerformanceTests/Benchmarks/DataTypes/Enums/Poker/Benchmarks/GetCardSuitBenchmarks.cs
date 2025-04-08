using BenchmarkDotNet.Attributes;
using PerformanceTests.Benchmarks.DataTypes.Enums.Poker;

namespace PerformanceTests.Benchmarks.DataTypes.Enums.Poker.Benchmarks;

public class GetCardSuitBenchmarks : CardNameBenchmarks
{

    [Benchmark]
    [ArgumentsSource(nameof(CardNameTestData))]
    public CardSuit GetCardSuit(CardName cardName) => CardMath.GetSuit(cardName);

    [Benchmark]
    [ArgumentsSource(nameof(CardNameTestData))]
    public CardSuit GetCardSuit2(CardName cardName) => CardMath.GetSuit2(cardName);

    [Benchmark]
    [ArgumentsSource(nameof(CardNameTestData))]
    public CardSuit GetSuitViaSuitLookup(CardName cardName) => SuitLookup.GetSuit(cardName);
}