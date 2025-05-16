using System.Diagnostics.CodeAnalysis;

namespace QuickTests.DataTypes.Enums.Names;

public enum MyEnum : byte
{
    None = 0,

    ValueA = 1,
    ValueB = 2,
    ValueC = 3,

    [SuppressMessage("ReSharper", "IdentifierTypo", Justification = "Demonstrates that enum values can have different casing")]
    Valuea = 255,


    A = ValueA,
    B = ValueB,
    C = ValueC,

    //Alternative name for None
    Empty = 0,
}