namespace Shared
{
    public static class Constants
    {
        public const int DefaultServerPort = 5000;


        /// <summary>
        /// The byte at which the length of the remaining data is written.
        /// </summary>
        public const int LengthStart = 0 + sizeof(int);

        /// <summary>
        /// The byte at which the data bytes start.
        /// </summary>
        public const int DataStart = HeaderLength;


        public const int HeaderLength = 1 + 1; // 1 Byte for message type, 1 byte for remaining byte length
        public const int MaxDataLength = byte.MaxValue;
        public const int MaxMessageLength = HeaderLength + MaxDataLength;

    }
}
