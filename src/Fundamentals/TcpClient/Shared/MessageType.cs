namespace Shared
{
    public enum MessageType : byte
    {
        Dummy = 0,
        SetBackgroundColor,
        SetForegroundColor,
        GetBackgroundColor,
        GetForegroundColor,
        GetBackgroundColorReply,
        GetForegroundColorReply,
    }
}