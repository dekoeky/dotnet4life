namespace QuickTests.DataTypes.Numeric_Types;

[TestClass]
public class GenericMathTests
{
    [TestMethod]
    [DynamicData(nameof(DoubleTestData))]
    public void CircleAreaDouble(double r, double expected)
    {
        // Act
        var result = GenericMathTestMethods.CircleAreaDouble(r);

        // Assert
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [DynamicData(nameof(FloatTestData))]
    public void CircleAreaFloat(float r, float expected)
    {
        // Act
        var result = GenericMathTestMethods.CircleAreaFloat(r);

        // Assert
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [DynamicData(nameof(DoubleTestData))]
    public void CircleAreaGenericDouble(double r, double expected)
    {
        // Act
        var result = GenericMathTestMethods.CircleAreaGeneric(r);

        // Assert
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [DynamicData(nameof(FloatTestData))]
    public void CircleAreaGenericFloat(float r, float expected)
    {
        // Act
        var result = GenericMathTestMethods.CircleAreaGeneric(r);

        // Assert
        Assert.AreEqual(expected, result);
    }

    public static IEnumerable<(double r, double expected)> DoubleTestData()
    {
        yield return (3d, 18.84955592153876d);
    }

    public static IEnumerable<(float r, float expected)> FloatTestData()
    {
        yield return (3f, 18.849556f);
    }
}