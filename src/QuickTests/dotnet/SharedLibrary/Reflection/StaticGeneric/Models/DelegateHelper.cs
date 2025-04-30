namespace SharedLibrary.Reflection.StaticGeneric.Models;

public static class DelegateHelper
{
    public static DoubleOperationDelegate CreateDelegate<T>()
    {
        //Find the implemented interface
        var interfaceOnType =
            typeof(T)
                .GetInterfaces()
                //Assume we always only have a single implementation
                .Single(it => it.GetGenericTypeDefinition() == typeof(IDemo<,>));

        //Get the interface mapping for the current type
        var map = typeof(T).GetInterfaceMap(interfaceOnType);

        //Grab the interface method, assuming there is only one for now
        var targetMethod = map.TargetMethods.First();

        //Convert it into a delegate
        return targetMethod.CreateDelegate<DoubleOperationDelegate>();
    }
    public static Func<double, double> CreateFunc<T>()
    {
        //Find the implemented interface
        var interfaceOnType =
            typeof(T)
                .GetInterfaces()
                //Assume we always only have a single implementation
                .Single(it => it.GetGenericTypeDefinition() == typeof(IDemo<,>));

        //Get the interface mapping for the current type
        var map = typeof(T).GetInterfaceMap(interfaceOnType);

        //Grab the interface method, assuming there is only one for now
        var targetMethod = map.TargetMethods.First();

        //Convert it into a delegate
        return targetMethod.CreateDelegate<Func<double, double>>();
    }
}