namespace QuickTests.ThirdParty.CsvHelper;

/// <summary>
/// A simple implementation of <see cref="IPerson"/>, without <see cref="CsvHelper"/> attributes.
/// </summary>
public class Person : IPerson
{
    public int Id { get; init; }

    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public DateOnly BirthDate { get; init; }

    public string Email { get; init; } = string.Empty;

    public double Salary { get; init; }
}