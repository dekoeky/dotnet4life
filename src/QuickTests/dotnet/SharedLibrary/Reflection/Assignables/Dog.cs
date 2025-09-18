namespace SharedLibrary.Reflection.Assignables;

internal class Dog(string name) : Animal(name)
{
    public override void Speak() => Console.WriteLine("Woof");
}