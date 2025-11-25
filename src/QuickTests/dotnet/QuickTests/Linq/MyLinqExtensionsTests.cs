namespace QuickTests.Linq;

/// <summary>
/// <seealso cref="MyLinqExtensions"/> related tests.
/// </summary>
[TestClass]
public class MyLinqExtensionsTests
{
    private static readonly Person[] People = [
        new () { Name  = "Bob", Age = 36 },
        new () { Name = "Alice", Age = 42 },
        new () { Name = "Eric", Age = 90 },
        new () { Name = "John", Age = 28 },
        new () { Name = "Jane", Age = 17 },
        new () { Name = "Erica", Age = 81 },
    ];

    private const string NameOfOldestPerson = "Eric";
    private const string NameOfYoungestPerson = "Jane";


    /// <summary>
    /// <see cref="MyLinqExtensions.MinMaxBy{TSource,TKey}(System.Collections.Generic.IEnumerable{TSource},System.Func{TSource,TKey})"/> demonstration.
    /// </summary>
    /// <seealso cref="Enumerable.MinBy{TSource,TKey}(System.Collections.Generic.IEnumerable{TSource},System.Func{TSource,TKey})"/>
    /// <seealso cref="Enumerable.MaxBy{TSource,TKey}(System.Collections.Generic.IEnumerable{TSource},System.Func{TSource,TKey})"/>
    [TestMethod]
    public void MinMaxBy()
    {
        //ACT
        var both = People.MinMaxBy(p => p.Age);

        //ASSERT
        Assert.AreEqual(NameOfOldestPerson, both.max?.Name);
        Assert.AreEqual(NameOfYoungestPerson, both.min?.Name);
    }

    /// <summary>
    /// <see cref="Enumerable.MinBy{TSource,TKey}(System.Collections.Generic.IEnumerable{TSource},System.Func{TSource,TKey})"/> demonstration.
    /// </summary>
    [TestMethod]
    public void MinBy()
    {
        //ACT
        var youngest = People.MinBy(p => p.Age);

        //ASSERT
        Assert.AreEqual(NameOfYoungestPerson, youngest?.Name);
    }

    /// <summary>
    /// <see cref="Enumerable.MaxBy{TSource,TKey}(System.Collections.Generic.IEnumerable{TSource},System.Func{TSource,TKey})"/> demonstration.
    /// </summary>
    [TestMethod]
    public void MaxBy()
    {
        //ACT
        var oldest = People.MaxBy(p => p.Age);

        //ASSERT
        Assert.AreEqual(NameOfOldestPerson, oldest?.Name);
    }

    private record Person
    {
        public required string Name { get; init; }
        public int Age { get; init; }
    }
}