namespace MyTool.Commands.Root.Math;

internal class AddCommand() : SimpleMathCommandBase("add", "Add two values")
{
    protected override double Calculate(double a, double b) => a + b;

    protected override char OperatorChar => '+';
}