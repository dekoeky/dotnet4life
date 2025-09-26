namespace SharedLibrary.Techorama._2025;

/// <summary>
/// <seealso cref="string"/> extensions related to <see cref="CustomStringAwaiter"/>.
/// </summary>
public static class CustomStringAwaiterExtensions
{
    public static CustomStringAwaiter GetAwaiter(this string? s) => new(s);
}