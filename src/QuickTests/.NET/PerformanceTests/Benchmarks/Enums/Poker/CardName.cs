using System.ComponentModel;

namespace PerformanceTests.Benchmarks.Enums.Poker;

/// <summary>
/// The full name of a poker card.
/// </summary>
public enum CardName : byte
{
    /// <summary>
    /// Ace Of Spades
    /// </summary>
    [Description("Ace Of Spades")]
    AceOfSpades = CardRank.Ace | CardSuit.Spades,

    /// <summary>
    /// Ace Of Hearts
    /// </summary>
    [Description("Ace Of Hearts")]
    AceOfHearts = CardRank.Ace | CardSuit.Hearts,

    /// <summary>
    /// Ace Of Diamonds
    /// </summary>
    [Description("Ace Of Diamonds")]
    AceOfDiamonds = CardRank.Ace | CardSuit.Diamonds,

    /// <summary>
    /// Ace Of Clubs
    /// </summary>
    [Description("Ace Of Clubs")]
    AceOfClubs = CardRank.Ace | CardSuit.Clubs,

    /// <summary>
    /// King Of Spades
    /// </summary>
    [Description("King Of Spades")]
    KingOfSpades = CardRank.King | CardSuit.Spades,

    /// <summary>
    /// Kind Of Hearts
    /// </summary>
    [Description("King Of Hearts")]
    KingOfHearts = CardRank.King | CardSuit.Hearts,

    /// <summary>
    /// King Of Diamonds
    /// </summary>
    [Description("King Of Diamonds")]
    KingOfDiamonds = CardRank.King | CardSuit.Diamonds,

    /// <summary>
    /// King Of Clubs
    /// </summary>
    [Description("King Of Clubs")]
    KingOfClubs = CardRank.King | CardSuit.Clubs,

    /// <summary>
    /// Queen Of Spades
    /// </summary>
    [Description("Queen Of Spades")]
    QueenOfSpades = CardRank.Queen | CardSuit.Spades,

    /// <summary>
    /// Queen Of Hearts
    /// </summary>
    [Description("Queen Of Hearts")]
    QueenOfHearts = CardRank.Queen | CardSuit.Hearts,

    /// <summary>
    /// Queen Of Diamonds
    /// </summary>
    [Description("Queen Of Diamonds")]
    QueenOfDiamonds = CardRank.Queen | CardSuit.Diamonds,

    /// <summary>
    /// Queen Of Clubs
    /// </summary>
    [Description("Queen Of Clubs")]
    QueenOfClubs = CardRank.Queen | CardSuit.Clubs,

    /// <summary>
    /// Jack Of Spades
    /// </summary>
    [Description("Jack Of Spades")]
    JackOfSpades = CardRank.Jack | CardSuit.Spades,

    /// <summary>
    /// Jack Of Hearts
    /// </summary>
    [Description("Jack Of Hearts")]
    JackOfHearts = CardRank.Jack | CardSuit.Hearts,

    /// <summary>
    /// Jack Of Diamonds
    /// </summary>
    [Description("Jack Of Diamonds")]
    JackOfDiamonds = CardRank.Jack | CardSuit.Diamonds,

    /// <summary>
    /// Jack Of Clubs
    /// </summary>
    [Description("Jack Of Clubs")]
    JackOfClubs = CardRank.Jack | CardSuit.Clubs,

    /// <summary>
    /// Ten Of Spades
    /// </summary>
    [Description("Ten Of Spades")]
    TenOfSpades = CardRank.Ten | CardSuit.Spades,

    /// <summary>
    /// Ten Of Hearts
    /// </summary>
    [Description("Ten Of Hearts")]
    TenOfHearts = CardRank.Ten | CardSuit.Hearts,

    /// <summary>
    /// Ten Of Diamonds
    /// </summary>
    [Description("Ten Of Diamonds")]
    TenOfDiamonds = CardRank.Ten | CardSuit.Diamonds,

    /// <summary>
    /// Ten Of Clubs
    /// </summary>
    [Description("Ten Of Clubs")]
    TenOfClubs = CardRank.Ten | CardSuit.Clubs,

    /// <summary>
    /// Nine Of Spades
    /// </summary>
    [Description("Nine Of Spades")]
    NineOfSpades = CardRank.Nine | CardSuit.Spades,

    /// <summary>
    /// Nine Of Hearts
    /// </summary>
    [Description("Nine Of Hearts")]
    NineOfHearts = CardRank.Nine | CardSuit.Hearts,

    /// <summary>
    /// Nine Of Diamonds
    /// </summary>
    [Description("Nine Of Diamonds")]
    NineOfDiamonds = CardRank.Nine | CardSuit.Diamonds,

    /// <summary>
    /// Nine Of Clubs
    /// </summary>
    [Description("Nine Of Clubs")]
    NineOfClubs = CardRank.Nine | CardSuit.Clubs,

    /// <summary>
    /// Eight Of Spades
    /// </summary>
    [Description("Eight Of Spades")]
    EightOfSpades = CardRank.Eight | CardSuit.Spades,

    /// <summary>
    /// Eight Of Hearts
    /// </summary>
    [Description("Eight Of Hearts")]
    EightOfHearts = CardRank.Eight | CardSuit.Hearts,

    /// <summary>
    /// Eight Of Diamonds
    /// </summary>
    [Description("Eight Of Diamonds")]
    EightOfDiamonds = CardRank.Eight | CardSuit.Diamonds,

    /// <summary>
    /// Eight Of Clubs
    /// </summary>
    [Description("Eight Of Clubs")]
    EightOfClubs = CardRank.Eight | CardSuit.Clubs,

    /// <summary>
    /// Seven Of Spades
    /// </summary>
    [Description("Seven Of Spades")]
    SevenOfSpades = CardRank.Seven | CardSuit.Spades,

    /// <summary>
    /// Seven Of Hearts
    /// </summary>
    [Description("Seven Of Hearts")]
    SevenOfHearts = CardRank.Seven | CardSuit.Hearts,

    /// <summary>
    /// Seven Of Diamonds
    /// </summary>
    [Description("Seven Of Diamonds")]
    SevenOfDiamonds = CardRank.Seven | CardSuit.Diamonds,

    /// <summary>
    /// Seven Of Clubs
    /// </summary>
    [Description("Seven Of Clubs")]
    SevenOfClubs = CardRank.Seven | CardSuit.Clubs,

    /// <summary>
    /// Six Of Spades
    /// </summary>
    [Description("Six Of Spades")]
    SixOfSpades = CardRank.Six | CardSuit.Spades,

    /// <summary>
    /// Six Of Hearts
    /// </summary>
    [Description("Six Of Hearts")]
    SixOfHearts = CardRank.Six | CardSuit.Hearts,

    /// <summary>
    /// Six Of Diamonds
    /// </summary>
    [Description("Six Of Diamonds")]
    SixOfDiamonds = CardRank.Six | CardSuit.Diamonds,

    /// <summary>
    /// Six Of Clubs
    /// </summary>
    [Description("Six Of Clubs")]
    SixOfClubs = CardRank.Six | CardSuit.Clubs,

    /// <summary>
    /// Five Of Spades
    /// </summary>
    [Description("Five Of Spades")]
    FiveOfSpades = CardRank.Five | CardSuit.Spades,

    /// <summary>
    /// Five Of Hearts
    /// </summary>
    [Description("Five Of Hearts")]
    FiveOfHearts = CardRank.Five | CardSuit.Hearts,

    /// <summary>
    /// Five Of Diamonds
    /// </summary>
    [Description("Five Of Diamonds")]
    FiveOfDiamonds = CardRank.Five | CardSuit.Diamonds,

    /// <summary>
    /// Five Of Clubs
    /// </summary>
    [Description("Five Of Clubs")]
    FiveOfClubs = CardRank.Five | CardSuit.Clubs,

    /// <summary>
    /// Four Of Spades
    /// </summary>
    [Description("Four Of Spades")]
    FourOfSpades = CardRank.Four | CardSuit.Spades,

    /// <summary>
    /// Four Of Hearts
    /// </summary>
    [Description("Four Of Hearts")]
    FourOfHearts = CardRank.Four | CardSuit.Hearts,

    /// <summary>
    /// Four Of Diamonds
    /// </summary>
    [Description("Four Of Diamonds")]
    FourOfDiamonds = CardRank.Four | CardSuit.Diamonds,

    /// <summary>
    /// Four Of Clubs
    /// </summary>
    [Description("Four Of Clubs")]
    FourOfClubs = CardRank.Four | CardSuit.Clubs,

    /// <summary>
    /// Three Of Spades
    /// </summary>
    [Description("Three Of Spades")]
    ThreeOfSpades = CardRank.Three | CardSuit.Spades,

    /// <summary>
    /// Three Of Hearts
    /// </summary>
    [Description("Three Of Hearts")]
    ThreeOfHearts = CardRank.Three | CardSuit.Hearts,

    /// <summary>
    /// Three Of Diamonds
    /// </summary>
    [Description("Three Of Diamonds")]
    ThreeOfDiamonds = CardRank.Three | CardSuit.Diamonds,

    /// <summary>
    /// Three Of Clubs
    /// </summary>
    [Description("Three Of Clubs")]
    ThreeOfClubs = CardRank.Three | CardSuit.Clubs,

    /// <summary>
    /// Two Of Spades
    /// </summary>
    [Description("Two Of Spades")]
    TwoOfSpades = CardRank.Two | CardSuit.Spades,

    /// <summary>
    /// Two Of Hearts
    /// </summary>
    [Description("Two Of Hearts")]
    TwoOfHearts = CardRank.Two | CardSuit.Hearts,

    /// <summary>
    /// Two Of Diamonds
    /// </summary>
    [Description("Two Of Diamonds")]
    TwoOfDiamonds = CardRank.Two | CardSuit.Diamonds,

    /// <summary>
    /// Two Of Clubs
    /// </summary>
    [Description("Two Of Clubs")]
    TwoOfClubs = CardRank.Two | CardSuit.Clubs,
}