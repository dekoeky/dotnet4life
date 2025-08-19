// ReSharper disable InconsistentNaming
namespace SharedLibrary.DataTypes.Enums.Flags;

/// <summary>
/// Byte-sized flags enum.
/// </summary>
[Flags]
public enum MyBits : byte
{
    //Flags
    Bit0 = 1 << 0,
    Bit1 = 1 << 1,
    Bit2 = 1 << 2,
    Bit3 = 1 << 3,
    Bit4 = 1 << 4,
    Bit5 = 1 << 5,
    Bit6 = 1 << 6,
    Bit7 = 1 << 7,

    //WellKnown Values
    None = 0,
    All_ = 0xFF, // _ = for equal length
}