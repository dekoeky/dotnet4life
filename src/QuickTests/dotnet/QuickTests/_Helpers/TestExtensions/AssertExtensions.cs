namespace QuickTests._Helpers.TestExtensions;

internal static class AssertExtensions
{
    extension(Assert)
    {
        public static void IsInstanceOfType<T>(object value, out T result)
        {
            Assert.IsInstanceOfType<T>(value);
            result = (T)value;
        }
    }
}
