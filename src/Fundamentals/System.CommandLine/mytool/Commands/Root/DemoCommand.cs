using MyTool.Services.Demo.WarpDrive;
using System.CommandLine;

namespace MyTool.Commands.Root;

internal class DemoCommand : Command
{
    public DemoCommand() : base("demo", "A Demo Command")
    {
        this.SetHandler(WarpDriveDemo.Run);
    }
}