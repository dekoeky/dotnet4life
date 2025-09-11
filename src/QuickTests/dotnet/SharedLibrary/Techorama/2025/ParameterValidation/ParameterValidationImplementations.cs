namespace SharedLibrary.Techorama._2025.ParameterValidation;

internal static class ParameterValidationImplementations
{
    public static IEnumerable<int> Incorrect(int count)
    {
        // This method only throws the exception when the first item is requested,
        // not when the method is called,
        // which is not idiomatic.
        ArgumentOutOfRangeException.ThrowIfNegative(count);

        for (var i = 0; i < count; i++)
            yield return i;
    }

    public static IEnumerable<int> Correct_Using_LocalFunction(int count)
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

    public static IEnumerable<int> Correct_Using_ExternalMethod(int count)
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