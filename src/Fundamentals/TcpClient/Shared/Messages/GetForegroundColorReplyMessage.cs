using System;

namespace Shared.Messages
{
    public class GetForegroundColorReplyMessage : MessageBase.WithData
    {
        public readonly ConsoleColor Color;

        public GetForegroundColorReplyMessage(ConsoleColor color) : base(MessageType.GetForegroundColorReply)
        {
            Color = color;
        }

        protected override byte WriteData(Span<byte> data) => data.WriteConsoleColor(Color);
    }
}