using System.CommandLine;

namespace MyTool.Commands.Root.Math;

internal abstract class SimpleMathCommandBase : Command
{
    protected readonly Argument<double> ArgumentA = new("a", "Value A");
    protected readonly Argument<double> ArgumentB = new("b", "Value B");
    public SimpleMathCommandBase(string name, string? description) : base(name, description)
    {
        AddArgument(ArgumentA);
        AddArgument(ArgumentB);
        this.SetHandler(PerformMath, ArgumentA, ArgumentB);
    }

    private void PerformMath(double a, double b)
    {
        try
        {
            var result = Calculate(a, b);
            Console.WriteLine($"{a} {OperatorChar} {b} = {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error calculating result: {ex}");
        }
    }

    protected abstract double Calculate(double a, double b);
    protected abstract char OperatorChar { get; }
}