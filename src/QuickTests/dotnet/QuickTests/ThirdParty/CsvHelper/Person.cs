namespace QuickTests.ThirdParty.CsvHelper;

/// <summary>
/// A simple implementation of <see cref="IPerson"/>, without <see cref="CsvHelper"/> attributes.
/// </summary>
public class Person : IPerson
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateOnly BirthDate { get; set; }

    public string Email { get; set; } = string.Empty;

    public double Salary { get; set; }
}