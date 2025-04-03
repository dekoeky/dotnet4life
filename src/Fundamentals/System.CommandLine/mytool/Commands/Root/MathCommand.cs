using MyTool.Commands.Root.Math;
using System.CommandLine;
using System.Globalization;

namespace MyTool.Commands.Root;

internal class MathCommand : Command
{
    public MathCommand() : base("math", "A set of Mathematical tools")
    {
        //Let's just have an easy workaround for . and ,
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        AddCommand(new AddCommand());
        AddCommand(new SubtractCommand());
        AddCommand(new MultiplyCommand());
        AddCommand(new DivideCommand());
    }
}