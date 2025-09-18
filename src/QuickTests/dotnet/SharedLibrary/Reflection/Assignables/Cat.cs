namespace SharedLibrary.Reflection.Assignables;

internal class Cat(string name) : Animal(name)
{
    public override void Speak() => Console.WriteLine("Meow");
}