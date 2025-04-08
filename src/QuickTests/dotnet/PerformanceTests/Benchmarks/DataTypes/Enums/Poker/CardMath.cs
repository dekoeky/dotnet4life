using static PerformanceTests.Benchmarks.DataTypes.Enums.Poker.CardNameConstants;

namespace PerformanceTests.Benchmarks.DataTypes.Enums.Poker;

public static class CardMath
{
    /// <summary>
    /// Provides the correct <see cref="CardName"/> for the combination of the provided <paramref name="suit"/> and <paramref name="rank"/>.
    /// </summary>
    /// <param name="suit">The suit of the card.</param>
    /// <param name="rank">The rank of the card.</param>
    /// <returns></returns>
    public static CardName GetCardName(CardSuit suit, CardRank rank) => (CardName)((int)suit | (int)rank);

    /// <summary>
    /// Returns the Suit of the provided <see cref="CardName"/>
    /// </summary>
    /// <param name="name">The name of the card.</param>
    /// <returns>The suit of the card.</returns>
    public static CardSuit GetSuit(CardName name) => (CardSuit)((int)name & MaskSuit);
    public static CardSuit GetSuit2(CardName name) => (CardSuit)((byte)name & MaskSuit);

    /// <summary>
    /// Returns the Rank of the provided <see cref="CardName"/>
    /// </summary>
    /// <param name="name">The name of the card.</param>
    /// <returns>The rank of the card.</returns>
    public static CardRank GetRank(CardName name) => (CardRank)((int)name & MaskRank);

    public static (CardRank rank, CardSuit suit) GetRankAndSuit(CardName name) => (GetRank(name), GetSuit(name));

    public static CardColor GetColor(CardSuit suit) => (CardColor)((int)suit & MaskColor);


    public static CardColor GetColor(CardName name) => (CardColor)((int)name & MaskColor);
}