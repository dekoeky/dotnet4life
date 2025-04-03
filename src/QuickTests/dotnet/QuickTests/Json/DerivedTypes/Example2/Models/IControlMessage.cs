using System.Text.Json.Serialization;

namespace QuickTests.Json.DerivedTypes.Example2.Models;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$cmd", UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization)]
[JsonDerivedType(typeof(SetBitControlMessage), SetBitControlMessage.TypeDiscriminator)]
[JsonDerivedType(typeof(SetFloatControlMessage), SetFloatControlMessage.TypeDiscriminator)]
[JsonDerivedType(typeof(RestartControlMessage), RestartControlMessage.TypeDiscriminator)]
public interface IControlMessage;