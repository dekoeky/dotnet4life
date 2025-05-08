using System.Runtime.CompilerServices;

namespace QuickTests.DataTypes.Numeric_Types;

[TestClass]
public abstract class NumericTestsBase<T>
{
    [TestMethod]
    public void SizeOf()
    {
        var typeName = typeof(T).Name;
        var bytes = Unsafe.SizeOf<T>();
        var bits = bytes * 8;

        Console.WriteLine($"Size of type '{typeName}':");
        Console.WriteLine($"  Bytes: {bytes}");
        Console.WriteLine($"  Bits:  {bits}");
    }
}