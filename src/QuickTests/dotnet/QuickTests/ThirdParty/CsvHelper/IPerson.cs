namespace QuickTests.ThirdParty.CsvHelper;

/// <summary>
/// Contract for test data related to persons.
/// </summary>
public interface IPerson
{
    int Id { get; set; }

    string FirstName { get; set; }

    string LastName { get; set; }

    DateOnly BirthDate { get; set; }

    string Email { get; set; }

    double Salary { get; set; }
}