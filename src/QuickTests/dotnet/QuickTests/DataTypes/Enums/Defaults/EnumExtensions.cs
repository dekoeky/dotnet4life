namespace QuickTests.DataTypes.Enums.Defaults;

public static class EnumExtensions
{
    public static string NumericAndRegularValue<T>(this T value) where T : struct, Enum => $"[{Convert.ToInt32(value)}] {value}";
}