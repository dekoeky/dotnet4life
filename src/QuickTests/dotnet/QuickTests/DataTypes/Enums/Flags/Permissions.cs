namespace QuickTests.DataTypes.Enums.Flags;

[Flags]
public enum Permissions
{
    //Flags
    Read = 1,
    Write = 2,
    Execute = 4,

    //Values (These are not 'flags', but combinations of flags combined into single values)
    None = 0,
    ReadWrite = Read | Write,
    All = Read | Write | Execute
}