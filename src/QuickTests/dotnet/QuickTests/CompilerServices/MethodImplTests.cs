using SharedLibrary.CompilerServices;

namespace QuickTests.CompilerServices;

[TestClass]
public class MethodImplTests
{
    [TestMethod]
    [DynamicData(nameof(TestData))]
    public void Inline(double r, double expected)
    {
        // Act
        var result = 4.0 / 3.0 * Math.PI * r * r * r;

        // Assert
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [DynamicData(nameof(TestData))]
    public void Regular(double r, double expected)
    {
        // Act
        var result = CompilerServicesDemo.Regular(r);

        // Assert
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [DynamicData(nameof(TestData))]
    public void AggressiveInlining(double r, double expected)
    {
        // Act
        var result = CompilerServicesDemo.AggressiveInlining(r);

        // Assert
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [DynamicData(nameof(TestData))]
    public void AggressiveOptimization(double r, double expected)
    {
        // Act
        var result = CompilerServicesDemo.AggressiveOptimization(r);

        // Assert
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [DynamicData(nameof(TestData))]
    public void NoInlining(double r, double expected)
    {
        // Act
        var result = CompilerServicesDemo.NoInlining(r);

        // Assert
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [DynamicData(nameof(TestData))]
    public void NoOptimization(double r, double expected)
    {
        // Act
        var result = CompilerServicesDemo.NoOptimization(r);

        // Assert
        Assert.AreEqual(expected, result);
    }

    public static IEnumerable<(double r, double expected)> TestData()
    {
        yield return (2, 33.510321638291124);
    }
}
