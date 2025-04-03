namespace MyTool.Commands.Root.Math;

internal class SubtractCommand : SimpleMathCommandBase
{
    public SubtractCommand() : base("subtract", "Subtract two values")
    {
        AddAlias("sub");
    }

    protected override double Calculate(double a, double b) => a - b;

    protected override char OperatorChar => '-';
}