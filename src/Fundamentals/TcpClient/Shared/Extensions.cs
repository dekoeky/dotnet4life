using System;
using System.Buffers.Binary;

namespace Shared
{
    internal static class Extensions
    {
        public static byte WriteConsoleColor(this Span<byte> data, ConsoleColor color)
        {
            BinaryPrimitives.WriteInt32BigEndian(data, (int)color);
            return sizeof(ConsoleColor);
        }
    }
}