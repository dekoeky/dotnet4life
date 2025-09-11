namespace QuickTests.FunctionalTechniques.Deconstruction.TestModels;

/// <summary>
/// Test model, that does not have a Deconstruct method defined.
/// </summary>
internal sealed class Plane
{
    public double Altitude { get; init; }
    public int Speed { get; init; }
}