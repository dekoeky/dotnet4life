namespace Shared.Messages
{
    public class GetForegroundColorMessage : MessageBase.WithoutData
    {
        public GetForegroundColorMessage() : base(MessageType.GetForegroundColor)
        {
        }
    }
}