namespace SharedLibrary.Reflection.StaticGeneric.Models;

public interface IDemo<TSelf, TOther>
    where TSelf : IDemo<TSelf, TOther>
{
    /// <summary>
    /// Executes the method on the implementing type.
    /// </summary>
    /// <seealso cref="DoubleOperationDelegate"/>
    static abstract double Execute(double value);
}