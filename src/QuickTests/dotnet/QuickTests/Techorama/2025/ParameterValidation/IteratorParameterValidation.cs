using Implementations = SharedLibrary.Techorama._2025.ParameterValidation.ParameterValidationImplementations;

namespace QuickTests.Techorama._2025.ParameterValidation;


/// <seealso cref="SharedLibrary.Techorama._2025.ParameterValidation.ParameterValidationImplementations"/>
[TestClass]
public class IteratorParameterValidation
{
    private const int BadCount = -10;
    private const int GoodCount = +10;

    [TestMethod]
    public void Incorrect_WithGoodParameter_ShouldNotThrow()
    {
        foreach (var value in Implementations.Incorrect(GoodCount))
            Console.WriteLine(value);
    }

    [TestMethod]
    public void Correct_Using_ExternalMethod_WithGoodParameter_ShouldNotThrow()
    {
        foreach (var value in Implementations.Correct_Using_ExternalMethod(GoodCount))
            Console.WriteLine(value);
    }

    [TestMethod]
    public void Correct_Using_LocalFunction_WithGoodParameter_ShouldNotThrow()
    {
        foreach (var value in Implementations.Correct_Using_LocalFunction(GoodCount))
            Console.WriteLine(value);
    }

    [TestMethod]
    public void Incorrect_WithBadParameter_ShouldThrowOnLoop()
    {
        // ---------- ARRANGE ----------
        var enumerable = Implementations.Incorrect(BadCount); //The exception is not thrown here yet

        // ---------- ASSERT -----------
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            // ---------- ACT --------------
            foreach (var value in enumerable) //The exception is only thrown here
                Console.WriteLine(value);
        });
    }

    [TestMethod]
    public void Correct_Using_ExternalMethod_WithBadParameter_ShouldThrowOnMethodCall()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var enumerable = Implementations.Correct_Using_ExternalMethod(BadCount); //The exception is already thrown here

            Assert.Fail("This point in the code will/should not be reached");

            foreach (var value in enumerable)
                Console.WriteLine(value);
        });
    }

    [TestMethod]
    public void Correct_Using_LocalFunction_WithBadParameter_ShouldThrowOnMethodCall()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var enumerable = Implementations.Correct_Using_LocalFunction(BadCount); //The exception is already thrown here

            Assert.Fail("This point in the code will/should not be reached");

            foreach (var value in enumerable)
                Console.WriteLine(value);
        });
    }
}