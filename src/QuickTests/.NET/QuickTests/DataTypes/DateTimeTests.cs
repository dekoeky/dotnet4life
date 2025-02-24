namespace QuickTests.DataTypes;

[TestClass]
public class DateTimeTests
{

    [TestMethod]
    public void UnspecifiedToLocalOrUtc()
    {
        //Arrange
        var dateTimes = new Dictionary<string, DateTime>
        {
            { "❌ unspecified", new DateTime(2025, 02, 21, 12, 00, 00, DateTimeKind.Unspecified)},
            { "✅ utc",         new DateTime(2025, 02, 21, 12, 00, 00, DateTimeKind.Utc)},
            { "✅ local",       new DateTime(2025, 02, 21, 12, 00, 00, DateTimeKind.Local)},
        };

        foreach (var (key, value) in dateTimes)
        {
            //Act
            var toUtc = value.ToUniversalTime();
            var toLocal = value.ToLocalTime();

            //Assert
            Console.WriteLine($"{key}:");
            Console.WriteLine($"Original: {ToDebugString(value)}");
            Console.WriteLine($"ToLocal:  {ToDebugString(toLocal)}");
            Console.WriteLine($"ToUtc:    {ToDebugString(toUtc)}");
            Console.WriteLine();
        }

        return;

        static string ToDebugString(DateTime d) => $"{d} [{d.Kind}]";
    }
}