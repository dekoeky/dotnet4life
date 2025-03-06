using System.ComponentModel;

namespace PerformanceTests.Benchmarks.Enums.Poker;

/// <summary>
/// The color of a poker card.
/// </summary>
public enum CardColor
{
    /// <summary>
    /// Black Color
    /// </summary>
    [Description("Black")]
    Black = 0b000000,

    /// <summary>
    /// Red Color
    /// </summary>
    [Description("Red")]
    Red = 0b100000,
}