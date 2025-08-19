using System.Runtime.CompilerServices;

namespace SharedLibrary.DataTypes.Enums.Flags;

/// <summary>
/// Methods under test for <see cref="MyBits"/>.
/// </summary>
public static class MyBitsMethodsForTesting
{
    public static MyBits SetFlag(MyBits value, MyBits flagToSet) => value | flagToSet;
    public static void SetFlagByRef(ref MyBits value, MyBits flagToSet) => value |= flagToSet;
    public static void SetFlagByRefUnsafe<T>(ref T value, T flag) where T : unmanaged, Enum
    {
        var val = Unsafe.As<T, ulong>(ref value);
        var flg = Unsafe.As<T, ulong>(ref flag);
        val |= flg;
        value = Unsafe.As<ulong, T>(ref val);
    }
}