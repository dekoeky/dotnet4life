namespace QuickTests.Json.DerivedTypes.Example2.Models;

public class SetBitControlMessage : IControlMessage
{
    internal const string TypeDiscriminator = "setBit";
    public int Bit { get; init; }
    public bool Value { get; init; }
}