namespace QuickTests.DataTypes.Nullable;

[TestClass]
public class NullableTests
{
    [TestMethod]
    public void NullCoalescingOperatorsCheck()
    {
        //Arrange
        SomeClass? o = null;

        //Assert
        Assert.IsTrue(o?.Boolean == null);
        Assert.IsTrue(o?.Boolean is null);

        Assert.IsFalse(o?.Boolean == true);
        Assert.IsFalse(o?.Boolean == false);

        Assert.IsTrue(o?.Boolean != true);
        Assert.IsTrue(o?.Boolean != false);
    }


    private class SomeClass
    {
        public bool Boolean { get; set; }
    }
}