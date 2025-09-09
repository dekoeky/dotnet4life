namespace SharedLibrary.Interfaces;

internal class ImplicitIntAdder : IIntAdder
{
    public int Add(int a, int b) => a + b;
}