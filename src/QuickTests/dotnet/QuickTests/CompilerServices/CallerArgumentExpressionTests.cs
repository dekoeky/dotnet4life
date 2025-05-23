using System.Runtime.CompilerServices;

namespace QuickTests.CompilerServices;

/// <summary>
/// <see cref="CallerArgumentExpressionAttribute"/> related tests.
/// </summary>
[TestClass]
public class CallerArgumentExpressionTests
{
    [TestMethod]
    public void FromProperty()
    {
        //ARRANGE
        var myData = new DataContainer();

        //ACT
        var result = MethodUnderTest(myData.MyProperty);

        //ASSERT
        Assert.AreEqual(nameof(DataContainer.MyProperty), result);
    }

    [TestMethod]
    public void FromVariableName()
    {
        //ARRANGE
        var myData = new DataElement();

        //ACT
        var result = MethodUnderTest(myData);

        //ASSERT
        Assert.AreEqual(nameof(myData), result);
    }

    [TestMethod]
    public void FromProvidedValue()
    {
        //ARRANGE
        const string dataName = "My Favorite Data";
        var myData = new DataElement();

        //ACT
        var result = MethodUnderTest(myData, dataName);

        //ASSERT
        Assert.AreEqual(dataName, result);
    }

    private static string MethodUnderTest(DataElement data, [CallerArgumentExpression(nameof(data))] string dataName = "")
    {
        // Do something with data
        _ = data;

        // Remove the type name from the caller argument expression
        return dataName.Split('.').Last();
    }

    #region Helper Classes

    /// <summary>
    /// Helper class to test passing a <see cref="DataElement"/> as a property ( <see cref="MyProperty"/> ).
    /// </summary>
    private class DataContainer
    {
        public DataElement MyProperty { get; set; } = new();
    }

    /// <summary>
    /// Helper class to test <see cref="MethodUnderTest"/>.
    /// </summary>
    private record DataElement;

    #endregion
}