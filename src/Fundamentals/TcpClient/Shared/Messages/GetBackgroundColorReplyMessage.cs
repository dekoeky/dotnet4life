using System;

namespace Shared.Messages
{
    public class GetBackgroundColorReplyMessage : MessageBase.WithData
    {
        public readonly ConsoleColor Color;

        public GetBackgroundColorReplyMessage(ConsoleColor color) : base(MessageType.GetBackgroundColorReply)
        {
            Color = color;
        }

        protected override byte WriteData(Span<byte> data) => data.WriteConsoleColor(Color);
    }
}