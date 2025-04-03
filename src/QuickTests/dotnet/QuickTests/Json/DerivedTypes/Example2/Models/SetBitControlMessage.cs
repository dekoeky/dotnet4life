namespace QuickTests.Json.DerivedTypes.Example2.Models;

public class SetBitControlMessage : IControlMessage
{
    internal const string TypeDiscriminator = "setBit";
    public int Bit { get; set; }
    public bool Value { get; set; }
}