namespace QuickTests.Reflection.StaticGeneric.Models;

interface IDemo<TSelf, TOther>
    where TSelf : IDemo<TSelf, TOther>
{
    static abstract double TimesTwo(double value);

}