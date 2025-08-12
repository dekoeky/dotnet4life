namespace Shared.Messages
{
    public class DummyMessage : MessageBase.WithoutData
    {
        public DummyMessage() : base(MessageType.Dummy)
        {
        }
    }
}