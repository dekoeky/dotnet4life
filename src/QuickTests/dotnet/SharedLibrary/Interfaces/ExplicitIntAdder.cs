namespace SharedLibrary.Interfaces;

internal class ExplicitIntAdder : IIntAdder
{
    int IIntAdder.Add(int a, int b) => Add(a, b);

    public int Add(int a, int b) => a + b;
}