namespace PerformanceTests.Benchmarks.Enums.Poker;

public static class SuitLookup
{
    private static readonly CardSuit[] Lu = new CardSuit[256];

    static SuitLookup()
    {
        for (var i = 0; i < 256; i++) Lu[i] = (CardSuit)(i & CardNameConstants.MaskSuit);
    }

    public static CardSuit GetSuit(CardName name) => Lu[(byte)name];
}