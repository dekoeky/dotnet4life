using SharedLibrary.TestModels;

namespace QuickTests;

[TestClass]
public class InternalsVisibleToTests
{
    /// <summary>
    /// This test checks if the internal method <see cref="SomePublicClass.SomeInternalMethod"/> of <see cref="SomePublicClass"/> can be accessed from this test project.
    /// </summary>
    [TestMethod]
    public void TestMethod1()
    {
        //ARRANGE
        var unitUnderTest = new SomePublicClass();

        //ACT
        //The following internal method should be accessible due to either:
        //  - an InternalsVisibleTo attribute in the SharedLibrary project, in for example InternalsVisibleTo.cs.
        //    For example: [assembly: System.Runtime.CompilerServices.InternalsVisibleTo("QuickTests")]
        //  - or by <InternalsVisibleTo Include="QuickTests" /> entry in the SharedLibrary.csproj file.
        var result = unitUnderTest.SomeInternalMethod();

        //ASSERT
        Assert.IsFalse(string.IsNullOrEmpty(result));
    }
}