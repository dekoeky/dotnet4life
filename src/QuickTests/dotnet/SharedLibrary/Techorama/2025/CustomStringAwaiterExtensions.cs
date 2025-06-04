namespace SharedLibrary.Techorama._2025;

public static class CustomStringAwaiterExtensions
{
    public static CustomStringAwaiter GetAwaiter(this string? s) => new(s);
}