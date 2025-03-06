using System.ComponentModel;

namespace PerformanceTests.Benchmarks.Enums.Poker;

/// <summary>
/// The suit of a poker card
/// </summary>
public enum CardSuit : byte
{
    /// <summary>
    /// Hearts
    /// </summary>
    [Description("Hearts")]
    Hearts = 0b110000,

    /// <summary>
    /// Diamonds
    /// </summary>
    [Description("Diamonds")]
    Diamonds = 0b100000,

    /// <summary>
    /// Spades
    /// </summary>
    [Description("Spades")]
    Spades = 0b010000,

    /// <summary>
    /// Clubs
    /// </summary>
    [Description("Clubs")]
    Clubs = 0b000000,
}