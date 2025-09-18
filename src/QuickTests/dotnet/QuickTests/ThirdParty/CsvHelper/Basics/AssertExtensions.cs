namespace QuickTests.ThirdParty.CsvHelper.Basics;

internal static class AssertExtensions
{
    public static void IsExpectedTestData<T>(this Assert _, ICollection<T> records) where T : IPerson
    {
        var expected = PersonTestData.IPersons;

        Assert.AreEqual(expected.Length, records.Count);

        for (var i = 0; i < expected.Length; i++)
        {
            var e = expected[i];
            var r = records.ElementAt(i) as IPerson;

            Assert.AreEqual(e.Id, r.Id);
            Assert.AreEqual(e.FirstName, r.FirstName);
            Assert.AreEqual(e.LastName, r.LastName);
            Assert.AreEqual(e.BirthDate, r.BirthDate);
            Assert.AreEqual(e.Email, r.Email);
            Assert.AreEqual(e.Salary, r.Salary);
        }
    }
}