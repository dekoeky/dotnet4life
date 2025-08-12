using System;

namespace Shared.Messages
{
    public abstract class MessageBase
    {
        private readonly MessageType _messageType;

        private MessageBase(MessageType messageType)
        {
            _messageType = messageType;
        }

        protected abstract byte WriteData(Span<byte> data);

        public byte[] GetBytes()
        {
            Span<byte> bytes = stackalloc byte[Constants.MaxMessageLength];

            //Write the data (starting at data start index) and the data length
            var dataLength = WriteData(bytes[Constants.DataStart..]);

            //Complete the header
            bytes[0] = (byte)_messageType;
            bytes[1] = dataLength;

            //Return (used) bytes
            return bytes[..(Constants.HeaderLength + dataLength)].ToArray();
        }

        public abstract class WithData : MessageBase
        {
            protected WithData(MessageType type) : base(type)
            {
            }
        }
        public abstract class WithoutData : MessageBase
        {
            protected WithoutData(MessageType type) : base(type)
            {
            }

            protected sealed override byte WriteData(Span<byte> data) => 0;
        }
    }
}