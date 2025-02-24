namespace QuickTests.Json.DerivedTypes.Example2.Models;

public class RestartControlMessage : IAdminControlMessage
{
    internal const string TypeDiscriminator = "restart";
}