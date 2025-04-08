using BenchmarkDotNet.Attributes;
using PerformanceTests.Benchmarks.DataTypes.Enums.Poker;

namespace PerformanceTests.Benchmarks.DataTypes.Enums.Poker.Benchmarks;

public class GetCardColorBenchmarks : CardNameBenchmarks
{
    [Benchmark]
    [ArgumentsSource(nameof(CardNameTestData))]
    public CardColor GetCardColor_FromCardName(CardName cardName) => CardMath.GetColor(cardName);

    [Benchmark]
    [ArgumentsSource(nameof(CardSuitTestData))]
    public CardColor GetCardColor_FromCardSuit(CardSuit cardSuit) => CardMath.GetColor(cardSuit);
}