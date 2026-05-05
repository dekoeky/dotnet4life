using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace QuickTests.DataTypes.Collections;

[TestClass]
public class HashSetTests
{
    [TestMethod]
    [SuppressMessage("Style", "IDE0028:Simplify collection initialization", Justification = "Clear Arrange/Act separation")]
    public void KeepsOriginalOrder()
    {
        // Arrange
        var collection = new HashSet<int>();

        // Act
        collection.Add(3);
        collection.Add(1);
        collection.Add(5);

        // Assert
        foreach (var item in collection.Order())
            Debug.WriteLine(item);

        // We expect the first added element, to be the first enumerated element
        Assert.AreEqual(3, collection.First());
    }
}