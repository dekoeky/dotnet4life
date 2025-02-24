using System.Text.Json.Serialization;

namespace QuickTests.Json.DerivedTypes.Example2.Models;

[JsonConverter(typeof(InterfaceRedirectConverterFactory<IAdminControlMessage, IControlMessage>))]
public interface IAdminControlMessage : IControlMessage;