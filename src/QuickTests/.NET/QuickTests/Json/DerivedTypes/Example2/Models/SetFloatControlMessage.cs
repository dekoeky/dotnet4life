namespace QuickTests.Json.DerivedTypes.Example2.Models;

public class SetFloatControlMessage : IControlMessage
{
    internal const string TypeDiscriminator = "setFloat";
    public required string Name { get; set; }
    public float Value { get; set; }
}