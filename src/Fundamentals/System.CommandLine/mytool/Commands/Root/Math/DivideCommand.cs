namespace MyTool.Commands.Root.Math;

internal class DivideCommand : SimpleMathCommandBase
{
    public DivideCommand() : base("divide", "Divide two values")
    {
        AddAlias("div");
    }

    protected override double Calculate(double a, double b) => a / b;

    protected override char OperatorChar => '/';
}