namespace MyTool.Commands.Root.Math;

internal class MultiplyCommand : SimpleMathCommandBase
{
    public MultiplyCommand() : base("multiply", "Multiply two values")
    {
        AddAlias("mul");
    }

    protected override double Calculate(double a, double b) => a * b;

    protected override char OperatorChar => '*';
}