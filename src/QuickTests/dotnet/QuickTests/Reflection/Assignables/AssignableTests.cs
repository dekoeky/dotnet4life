namespace QuickTests.Reflection.Assignables;

[TestClass]
public class AssignableTests
{
    [TestMethod]
    [AssignablesTestDataSource]
    public void IsAssignableTo(Type type, Type baseType, bool expected)
    {
        // ---------- ACT --------------
        var result = type.IsAssignableTo(baseType);

        // ---------- ASSERT -----------
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [AssignablesTestDataSource]
    public void IsAssignableFrom(Type type, Type baseType, bool expected)
    {
        // ---------- ACT --------------
        var result = baseType.IsAssignableFrom(type);

        // ---------- ASSERT -----------
        Assert.AreEqual(expected, result);
    }
}