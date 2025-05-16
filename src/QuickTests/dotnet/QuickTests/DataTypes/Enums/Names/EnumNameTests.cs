namespace QuickTests.DataTypes.Enums.Names;

[TestClass]
public class EnumNameTests
{
    [DataTestMethod]
    [DataRow(MyEnum.None, MyEnum.Empty)]
    [DataRow(MyEnum.A, MyEnum.ValueA)]
    [DataRow(MyEnum.B, MyEnum.ValueB)]
    [DataRow(MyEnum.C, MyEnum.ValueC)]
    public void AreEqual(MyEnum a, MyEnum b) => Assert.AreEqual(a, b);

    [DataTestMethod]
    [DataRow(MyEnum.ValueA, MyEnum.Valuea)]
    public void AreNotEqual(MyEnum a, MyEnum b) => Assert.AreNotEqual(a, b);

    [DataTestMethod]
    [DataRow(nameof(MyEnum.None), MyEnum.None)]
    [DataRow(nameof(MyEnum.Empty), MyEnum.None)]
    [DataRow(nameof(MyEnum.ValueA), MyEnum.ValueA)]
    [DataRow(nameof(MyEnum.ValueB), MyEnum.ValueB)]
    [DataRow(nameof(MyEnum.ValueC), MyEnum.ValueC)]
    [DataRow(nameof(MyEnum.A), MyEnum.ValueA)]
    [DataRow(nameof(MyEnum.B), MyEnum.ValueB)]
    [DataRow(nameof(MyEnum.C), MyEnum.ValueC)]
    [DataRow(nameof(MyEnum.Valuea), MyEnum.Valuea)]
    public void ParsesTo(string str, MyEnum expected)
    {
        // ---------- ACT --------------
        var success = Enum.TryParse(str, out MyEnum result);

        // ---------- ASSERT -----------
        Assert.IsTrue(success);
        Assert.AreEqual(expected, result);
    }
}