using System;
using System.Buffers.Binary;

namespace Shared.Messages
{
    public class SetForegroundColorMessage : MessageBase.WithData
    {
        private const int DataLength = sizeof(ConsoleColor);

        public readonly ConsoleColor Color;

        public SetForegroundColorMessage(ConsoleColor color) : base(MessageType.SetForegroundColor)
        {
            Color = color;
        }

        protected override byte WriteData(Span<byte> data) => data.WriteConsoleColor(Color);

        public static SetForegroundColorMessage Parse(ReadOnlySpan<byte> bytes)
        {
            //TODO: Validate length?

            var color = (ConsoleColor)BinaryPrimitives.ReadInt32BigEndian(bytes);

            return new SetForegroundColorMessage(color);
        }
    }
}