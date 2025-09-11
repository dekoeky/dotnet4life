namespace QuickTests.FunctionalTechniques.Deconstruction.TestModels;

/// <summary>
/// Test model, that has (multiple) Deconstruct method(s) defined.
/// </summary>
internal class Car
{
    public float HorsePower { get; init; }
    public required string Color { get; init; }
    public int BuildYear { get; init; }

    /// <summary>
    /// Deconstruct with 3 (out) parameters.
    /// </summary>
    public void Deconstruct(out int buildYear, out string color, out float horsePower)
    {
        buildYear = BuildYear;
        color = Color;
        horsePower = HorsePower;
    }

    /// <summary>
    /// Deconstruct with 2 (out) parameters.
    /// </summary>
    public void Deconstruct(out int buildYear, out string color)
    {
        buildYear = BuildYear;
        color = Color;
    }
}