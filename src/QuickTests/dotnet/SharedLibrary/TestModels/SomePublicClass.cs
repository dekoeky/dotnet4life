namespace SharedLibrary.TestModels;

public class SomePublicClass
{
    /// <summary>
    /// An internal method that returns a greeting message.
    /// </summary>
    /// <remarks>
    /// This method is marked as internal, in order to test the InternalsVisibleTo attribute functionality.
    /// </remarks>
    internal string SomeInternalMethod() => $"Hello from {nameof(SomePublicClass)}.{nameof(SomeInternalMethod)}!";
}
