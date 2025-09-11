namespace QuickTests.FunctionalTechniques.Deconstruction.TestModels;

/// <summary>
/// <see cref="Plane"/> extensions.
/// </summary>
internal static class PlaneExtensions
{
    public static void Deconstruct(this Plane plane, out double altitude, out int speed)
    {
        altitude = plane.Altitude;
        speed = plane.Speed;
    }
}