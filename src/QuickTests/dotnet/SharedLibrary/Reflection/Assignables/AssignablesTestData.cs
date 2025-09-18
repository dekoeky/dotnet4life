namespace SharedLibrary.Reflection.Assignables;

internal static class AssignablesTestData
{
    public static IEnumerable<(Type Type, Type BaseType, bool Assignable)> Get()
    {
        yield return (typeof(Dog), typeof(Animal), true);
        yield return (typeof(Dog), typeof(IAnimal), true);
        yield return (typeof(Dog), typeof(Cat), false);

        yield return (typeof(Cat), typeof(Animal), true);
        yield return (typeof(Cat), typeof(IAnimal), true);
        yield return (typeof(Cat), typeof(Dog), false);

        yield return (typeof(Animal), typeof(IAnimal), true);
        yield return (typeof(Animal), typeof(Dog), false);
        yield return (typeof(Animal), typeof(Cat), false);

        yield return (typeof(IAnimal), typeof(Animal), false);
        yield return (typeof(IAnimal), typeof(Dog), false);
        yield return (typeof(IAnimal), typeof(Cat), false);
    }

    public static IEnumerable<object[]> AsObjects() =>
        Get().Select(o => new object[] { o.Type, o.BaseType, o.Assignable });
}