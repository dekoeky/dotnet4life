namespace QuickTests.DataTypes.Boxing;

[TestClass]
public class BoxingTests
{
    private const double Value = Math.PI;

    [TestMethod]
    public void BoxedValue_ShouldEqual_OriginalValue()
    {
        //ARRANGE
        var value = Value;
        var boxedValue = (object)value; // Boxing

        //ACT
        var result = value.Equals(boxedValue);

        //ASSERT
        Assert.IsTrue(result, "The boxed value should equal the original value.");
    }

    [TestMethod]
    public void OriginalValue_ShouldEqual_BoxedValue()
    {
        //ARRANGE
        var value = Value;
        var boxedValue = (object)value; // Boxing

        //ACT
        var result = boxedValue.Equals(value);

        //ASSERT
        Assert.IsTrue(result, "The original value should equal the boxed value.");
    }

    [TestMethod]
    public void TwoBoxedValues_ShouldBeEqual()
    {
        //ARRANGE
        var value = Value;
        var boxedValue1 = (object)value; // Boxing
        var boxedValue2 = (object)value; // Boxing

        //ACT
        var result = boxedValue1.Equals(boxedValue2);

        //ASSERT
        Assert.IsTrue(result, "The two boxing objects should be equal");
    }

    [TestMethod]
    public void BoxedValueHashCode_ShouldEqual_OriginalValueHashCode()
    {
        //ARRANGE
        var value = Value;
        var boxedValue = (object)value; // Boxing

        //ACT
        var valueHashCode = value.GetHashCode();
        var boxedValueHashCode = boxedValue.GetHashCode();

        //ASSERT
        Assert.AreEqual(valueHashCode, boxedValueHashCode, "The hash codes of the boxed value and the original value should be equal.");
    }
}