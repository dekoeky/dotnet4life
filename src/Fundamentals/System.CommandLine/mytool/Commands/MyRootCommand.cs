using MyTool.Commands.Root;
using System.CommandLine;

namespace MyTool.Commands;

internal class MyRootCommand : RootCommand
{
    public MyRootCommand() : base("Example CLI Tool")
    {
        AddCommand(new DemoCommand());
        AddCommand(new DateCommand());
        AddCommand(new MathCommand());
    }
}