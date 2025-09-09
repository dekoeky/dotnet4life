namespace SharedLibrary.Interfaces;

internal class ExplicitOnlyIntAdder : IIntAdder
{
    int IIntAdder.Add(int a, int b) => a + b;
}