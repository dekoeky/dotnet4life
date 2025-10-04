using System.Diagnostics.CodeAnalysis;


namespace QuickTests.DataTypes.Nullable;

[TestClass]
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
public class NullableTests
{
    [TestMethod]
    public void NullCoalescingOperatorsCheck()
    {
        //Arrange
        SomeClass? o = null;

        //Assert
#pragma warning disable MSTEST0037
        Assert.IsTrue(o?.Boolean == null);
        Assert.IsTrue(o?.Boolean is null);

        Assert.IsFalse(o?.Boolean == true);
        Assert.IsFalse(o?.Boolean == false);

        Assert.IsTrue(o?.Boolean != true);
        Assert.IsTrue(o?.Boolean != false);
#pragma warning restore MSTEST0037
    }


    private class SomeClass
    {
        public bool Boolean { get; set; }
    }
}