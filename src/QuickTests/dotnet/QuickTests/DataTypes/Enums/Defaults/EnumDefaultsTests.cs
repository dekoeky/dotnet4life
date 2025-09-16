namespace QuickTests.DataTypes.Enums.Defaults;

[TestClass]
public class EnumDefaultsTests
{
    [TestMethod] public void EnumAbcDefaults() => Test<EnumAbc>();
    [TestMethod] public void PosNegDefaults() => Test<PosNeg>();


    private static void Test<T>() where T : struct, Enum
    {
        // ---------- ACT --------------
        var defaultValue = default(T);
        var defaultValueDefined = Enum.IsDefined(defaultValue);
        var possibleValues = Enum.GetValues<T>()
            .Select(EnumExtensions.NumericAndRegularValue)
            .ToArray();

        // ---------- ASSERT -----------
        Console.WriteLine($"Default '{typeof(T).Name}' value: {defaultValue}");
        Console.WriteLine($"Default '{typeof(T).Name}' value is defined: {defaultValueDefined}");
        Console.WriteLine("Possible values:");
        foreach (var possibleValue in possibleValues) Console.WriteLine($"    {possibleValue}");
    }

    private enum EnumAbc
    {
        A,
        B,
        C,
    }

    private enum PosNeg
    {
        Positive = +1,
        Negative = -1,
    }
}