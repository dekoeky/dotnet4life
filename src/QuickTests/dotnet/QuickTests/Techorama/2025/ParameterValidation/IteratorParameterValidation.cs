namespace QuickTests.Techorama._2025.ParameterValidation;

[TestClass]
public class IteratorParameterValidation
{
    private const int BadCount = -10;
    private const int GoodCount = +10;

    [TestMethod]
    public void A_Loop_IncorrectMethod_WithGoodParameter_ShouldNotThrow()
    {
        foreach (var value in Incorrect(GoodCount))
            Console.WriteLine(value);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void B_Loop_IncorrectMethod_WithBadParameter_ShouldThrow()
    {
        foreach (var value in Incorrect(BadCount))
            Console.WriteLine(value);
    }

    // So far so good.
    // However, only retrieving the iterator does not throw the exception

    [TestMethod]
    public void C_Loop_IncorrectMethod_WithBadParameter_ShouldThrow()
    {
        //This method does not throw an exception when only called (not enumerated), which is not idiomatic.
        var iEnumerable = Incorrect(BadCount);

        Assert.That.Ignore(iEnumerable);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void D_Loop_CorrectMethodExternal_WithBadParameter_ShouldThrow()
    {
        var iEnumerable = Correct_Using_ExternalMethod(BadCount);

        Assert.That.Ignore(iEnumerable);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void E_Loop_CorrectMethodLocal_WithBadParameter_ShouldThrow()
    {
        var iEnumerable = Correct_Using_LocalFunction(BadCount);

        Assert.That.Ignore(iEnumerable);
    }



    [TestMethod]
    public void F_Loop_IncorrectMethod_WithGoodParameter_ShouldNotThrow()
    {
        foreach (var value in Correct_Using_ExternalMethod(GoodCount))
            Console.WriteLine(value);
    }

    [TestMethod]
    public void G_Loop_IncorrectMethod_WithBadParameter_ShouldNotThrow()
    {
        foreach (var value in Correct_Using_LocalFunction(GoodCount))
            Console.WriteLine(value);
    }

    private static IEnumerable<int> Incorrect(int count)
    {
        // This method only throws the exception when the first item is requested,
        // not when the method is called,
        // which is not idiomatic.
        ArgumentOutOfRangeException.ThrowIfNegative(count);

        for (var i = 0; i < count; i++)
            yield return i;
    }

    private static IEnumerable<int> Correct_Using_LocalFunction(int count)
    {
        // This method throws the exception when the method is called,
        // before any items are requested,
        // which is idiomatic and preferred.
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        return IteratorCore();

        // Local function to encapsulate the iterator logic.
        IEnumerable<int> IteratorCore()
        {
            for (var i = 0; i < count; i++)
                yield return i;
        }
    }

    private static IEnumerable<int> Correct_Using_ExternalMethod(int count)
    {
        // This method throws the exception when the method is called,
        // before any items are requested,
        // which is idiomatic and preferred.
        ArgumentOutOfRangeException.ThrowIfNegative(count);

        // Call an external method to encapsulate the iterator logic.
        return ExternalIteratorFunction(count);
    }

    private static IEnumerable<int> ExternalIteratorFunction(int count)
    {
        for (var i = 0; i < count; i++)
            yield return i;
    }
}