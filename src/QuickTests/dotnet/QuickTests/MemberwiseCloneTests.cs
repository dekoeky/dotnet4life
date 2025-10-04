
namespace QuickTests;

[TestClass]
public class MemberwiseCloneTests
{
    [TestMethod]
    public void ShallowCopy()
    {
        //ARRANGE
        var data = new DemoData
        {
            Message = "Hello World",
            Number = 42,
            NestedObject = new DemoNestedData
            {
                InnerMessage = "Inner World"
            },
            Timestamp = DateTime.Now
        };

        //ACT
        var copy = data.ShallowCopy();

        //ASSERT
        // Ensure it's a different object(reference)
        Assert.AreNotSame(data, copy);
        // Ensure properties are copied correctly
        Assert.AreEqual(data.Message, copy.Message);
        Assert.AreEqual(data.Number, copy.Number);
        Assert.AreEqual(data.NestedObject.InnerMessage, copy.NestedObject.InnerMessage);
        Assert.AreEqual(data.Timestamp, copy.Timestamp);
    }

    [TestMethod]
    public void ShallowCopy_ShowThat_ReferencesAreShared()
    {
        //ARRANGE
        var original = new DemoData
        {
            Message = "Hello World",
            Number = 42,
            NestedObject = new DemoNestedData
            {
                InnerMessage = "Inner World"
            },
            Timestamp = DateTime.Now
        };

        //ACT
        var copy = original.ShallowCopy();
        // Modify the nested object in the copy
        copy.NestedObject.InnerMessage = "Modified Inner World";

        //ASSERT
        Assert.That.IsShallowCopy(original.NestedObject, copy.NestedObject);
        Assert.That.IsShallowCopy(original.Message, copy.Message);
        Assert.That.IsDeepCopy(original.Number, copy.Number);
    }

    private class DemoData
    {
        public string? Message { get; init; }
        public int Number { get; init; }
        public required DemoNestedData NestedObject { get; init; }
        public DateTime Timestamp { get; init; }

        public DemoData ShallowCopy() => (DemoData)MemberwiseClone();
    }

    private class DemoNestedData
    {
        public string? InnerMessage { get; set; }
    }
}

file static class AssertExtensions
{
    public static void IsShallowCopy<T>(this Assert _, T original, T copy)
    {
        // Check that the values are equal
        Assert.AreEqual(original, copy);
        // Check that the references are the same, making it a shallow copy
        Assert.AreSame(original, copy);
    }

    public static void IsDeepCopy<T>(this Assert _, T original, T copy)
    {
        // Check that the values are equal
        Assert.AreEqual(original, copy);
        // Check that the references are not the same, making it a shallow copy
        Assert.AreNotSame(original, copy);
    }
}