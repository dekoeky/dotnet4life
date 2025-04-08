using System.ComponentModel;

namespace PerformanceTests.Benchmarks.DataTypes.Enums.Poker;

/// <summary>
/// The rank of a poker card
/// </summary>
public enum CardRank : byte
{
    /// <summary>
    /// King
    /// </summary>
    [Description("King")]
    King = 13,

    /// <summary>
    /// Queen
    /// </summary>
    [Description("Queen")]
    Queen = 12,

    /// <summary>
    /// Jack
    /// </summary>
    [Description("Jack")]
    Jack = 11,

    /// <summary>
    /// Ten
    /// </summary>
    [Description("Ten")]
    Ten = 10,

    /// <summary>
    /// Nine
    /// </summary>
    [Description("Nine")]
    Nine = 9,

    /// <summary>
    /// Eight
    /// </summary>
    [Description("Eight")]
    Eight = 8,

    /// <summary>
    /// Seven
    /// </summary>
    [Description("Seven")]
    Seven = 7,

    /// <summary>
    /// Six
    /// </summary>
    [Description("Six")]
    Six = 6,

    /// <summary>
    /// Five
    /// </summary>
    [Description("Five")]
    Five = 5,

    /// <summary>
    /// Four
    /// </summary>
    [Description("Four")]
    Four = 4,

    /// <summary>
    /// Three
    /// </summary>
    [Description("Three")]
    Three = 3,

    /// <summary>
    /// Two
    /// </summary>
    [Description("Two")]
    Two = 2,

    /// <summary>
    /// Ace
    /// </summary>
    [Description("Ace")]
    Ace = 14,
}