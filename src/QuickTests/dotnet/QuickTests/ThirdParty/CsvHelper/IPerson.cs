namespace QuickTests.ThirdParty.CsvHelper;

/// <summary>
/// Contract for test data related to persons.
/// </summary>
public interface IPerson
{
    int Id { get; init; }

    string FirstName { get; init; }

    string LastName { get; init; }

    DateOnly BirthDate { get; init; }

    string Email { get; init; }

    double Salary { get; init; }
}