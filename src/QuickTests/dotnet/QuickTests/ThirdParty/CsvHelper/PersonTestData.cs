namespace QuickTests.ThirdParty.CsvHelper;

/// <summary>
/// Helper class to generate <see cref="IPerson"/> testdata.
/// </summary>
public static class PersonTestData
{
    public static T[] Create<T>() where T : class, IPerson, new() =>
    [
        new()
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            BirthDate = new DateOnly(1990, 1, 1),
            Email = "john.doe@test.com",
            Salary = 45000.75
        },
        new()
        {
            Id = 2,
            FirstName = "Jane",
            LastName = "Smith",
            BirthDate = new DateOnly(1985, 5, 20),
            Email = "jane.smith@test.com",
            Salary = 52500.00
        },
        new()
        {
            Id = 3,
            FirstName = "Alice",
            LastName = "Johnson",
            BirthDate = new DateOnly(1992, 11, 5),
            Email = "alice.johnson@test.com",
            Salary = 61000.50
        },
        new()
        {
            Id = 4,
            FirstName = "Bob",
            LastName = "Brown",
            BirthDate = new DateOnly(1978, 3, 14),
            Email = "bob.brown@test.com",
            Salary = 72000.25
        },
        new()
        {
            Id = 5,
            FirstName = "Charlie",
            LastName = "Davis",
            BirthDate = new DateOnly(2000, 7, 30),
            Email = "charlie.davis@test.com",
            Salary = 38000.10
        }
    ];

    public static readonly IPerson[] IPersons = Create<Person>()
        .Cast<IPerson>()
        .ToArray();

    public const string Invariant = """
                                    Id,FirstName,LastName,BirthDate,Email,Salary
                                    1,John,Doe,01/01/1990,john.doe@test.com,45000.75
                                    2,Jane,Smith,05/20/1985,jane.smith@test.com,52500
                                    3,Alice,Johnson,11/05/1992,alice.johnson@test.com,61000.5
                                    4,Bob,Brown,03/14/1978,bob.brown@test.com,72000.25
                                    5,Charlie,Davis,07/30/2000,charlie.davis@test.com,38000.1
                                    """;

    public const string NlBe = """
                               Id;FirstName;LastName;BirthDate;Email;Salary
                               1;John;Doe;1/01/1990;john.doe@test.com;45000,75
                               2;Jane;Smith;20/05/1985;jane.smith@test.com;52500
                               3;Alice;Johnson;5/11/1992;alice.johnson@test.com;61000,5
                               4;Bob;Brown;14/03/1978;bob.brown@test.com;72000,25
                               5;Charlie;Davis;30/07/2000;charlie.davis@test.com;38000,1
                               """;

    public const string EnUs = """
                               Id,FirstName,LastName,BirthDate,Email,Salary
                               1,John,Doe,1/1/1990,john.doe@test.com,45000.75
                               2,Jane,Smith,5/20/1985,jane.smith@test.com,52500
                               3,Alice,Johnson,11/5/1992,alice.johnson@test.com,61000.5
                               4,Bob,Brown,3/14/1978,bob.brown@test.com,72000.25
                               5,Charlie,Davis,7/30/2000,charlie.davis@test.com,38000.1
                               """;
}