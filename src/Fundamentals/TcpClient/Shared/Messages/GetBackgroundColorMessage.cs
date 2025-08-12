namespace Shared.Messages
{
    public class GetBackgroundColorMessage : MessageBase.WithoutData
    {
        public GetBackgroundColorMessage() : base(MessageType.GetBackgroundColor)
        {
        }
    }
}