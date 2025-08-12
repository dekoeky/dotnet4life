using System;
using System.Buffers.Binary;

namespace Shared.Messages
{
    public class SetBackgroundColorMessage : MessageBase.WithData
    {
        private const int DataLength = sizeof(ConsoleColor);

        public readonly ConsoleColor Color;

        public SetBackgroundColorMessage(ConsoleColor color) : base(MessageType.SetBackgroundColor)
        {
            Color = color;
        }

        protected override byte WriteData(Span<byte> data) => data.WriteConsoleColor(Color);

        public static SetBackgroundColorMessage ParseFromDataBytes(ReadOnlySpan<byte> bytes)
        {
            //TODO: Validate length?

            var color = (ConsoleColor)BinaryPrimitives.ReadInt32BigEndian(bytes);

            return new SetBackgroundColorMessage(color);
        }
    }
}