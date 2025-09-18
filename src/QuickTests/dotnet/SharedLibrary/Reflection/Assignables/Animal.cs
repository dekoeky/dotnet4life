namespace SharedLibrary.Reflection.Assignables;

internal abstract class Animal(string name) : IAnimal
{
    public string Name { get; } = name;
    public abstract void Speak();
}