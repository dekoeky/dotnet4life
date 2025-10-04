namespace QuickTests.Json.DerivedTypes.Example2.Models;

public class SetFloatControlMessage : IControlMessage
{
    internal const string TypeDiscriminator = "setFloat";
    public required string Name { get; init; }
    public float Value { get; init; }
}