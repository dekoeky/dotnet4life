using System.CodeDom.Compiler;
using System.Globalization;

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
            var deltaUtc = toUtc - value;
            var deltaLocal = toLocal - value;
            var deltaLocalVsUtc = toLocal - toUtc;

            //Assert
            Console.WriteLine($"{key}:");
            Console.WriteLine($"Original:       {ToDebugString(value)}");
            Console.WriteLine($"ToLocal:        {ToDebugString(toLocal)} (delta: {deltaLocal})");
            Console.WriteLine($"ToUtc:          {ToDebugString(toUtc)} (delta: {deltaUtc})");
            Console.WriteLine($"Local vs Utc:   {deltaLocalVsUtc}");
            Console.WriteLine();
        }

        return;

        static string ToDebugString(DateTime dt) => $"{dt} [{dt.Kind}]";
    }

    [DataTestMethod]
    [DataRow("Europe/Brussels", "2025-03-30T02:30:00", true)]
    [DataRow("Europe/Brussels", "2025-03-30T01:30:00", false)]
    [DataRow("Europe/Brussels", "2025-03-30T03:30:00", false)]
    public void IsInvalidTime(string tz, string t, bool expectedInvalid)
    {
        // ARRANGE
        var tzi = TimeZoneInfo.FindSystemTimeZoneById(tz);
        // Parse as "Unspecified" (so it doesn't get auto-converted to local time)
        var dt = DateTime.Parse(t, null);


        // ACT
        // Convert to the target timezone
        bool invalid;
        try
        {
            var localDt = TimeZoneInfo.ConvertTime(dt, tzi);
            Console.WriteLine($"{localDt} ({localDt.Kind})");
            invalid = tzi.IsInvalidTime(localDt);
        }
        catch (ArgumentException e)
        {
            if (e.Message.Contains("The supplied DateTime represents an invalid time")) invalid = true;
            else throw;
        }

        // ASSERT
        Assert.AreEqual(expectedInvalid, invalid);
    }

    [DataTestMethod]
    [DataRow("Europe/Brussels")]
    public void Explain(string tz)
    {
        //Arrange
        var tzi = TimeZoneInfo.FindSystemTimeZoneById(tz);

        //Act
        Explain(tzi);
    }

    private static void Explain(TimeZoneInfo tzi)
    {
        using var writer = new IndentedTextWriter(Console.Out);

        writer.WriteLine($"TimeZone Id:           {tzi.Id}");
        writer.WriteLine($"TimeZone DisplayName:  {tzi.DisplayName}");
        writer.WriteLine($"Base UTC Offset:       {tzi.BaseUtcOffset}");

        if (tzi.SupportsDaylightSavingTime)
        {
            writer.WriteLine($"Daylight Savings Time: {tzi.DaylightName}");
            writer.WriteLine($"Standard Time:         {tzi.StandardName}");
            WriteRules(tzi, writer);
        }
        else
            writer.WriteLine("This TimeZone does not have Daylight Savings Time");
    }

    private static void WriteRules(TimeZoneInfo tzi, IndentedTextWriter writer)
    {
        var rules = tzi.GetAdjustmentRules();
        writer.WriteLine(rules.Length switch
        {
            0 => "No Adjustment Rules",
            1 => "Single Adjustment Rule:",
            _ => "Adjustment Rules :"
        });

        var i = 1;
        foreach (var rule in rules)
        {
            if (rules.Length > 1) writer.WriteLine($"Rule {i++}:");
            writer.Indent++;
            Explain(writer, tzi, rule);
            writer.Indent--;
            writer.WriteLine();
        }
    }

    private static void Explain(IndentedTextWriter writer, TimeZoneInfo tzi, TimeZoneInfo.AdjustmentRule rule)
    {
        var year = DateTime.Now.Year;
        //rule.DateStart;
        //rule.DateEnd;

        //writer.WriteLine($"Throughout the year, {tzi.DisplayName} TimeZone has a base offset of {tzi.BaseUtcOffset} towards UTC");

        writer.WriteLine($"The DST starts on {Explain(rule.DaylightTransitionStart)}");

        var start = CalculateTransitionDate(year, rule.DaylightTransitionStart);
        var nonDstOffset = tzi.BaseUtcOffset + rule.BaseUtcOffsetDelta;
        var dstOffset = tzi.BaseUtcOffset + rule.BaseUtcOffsetDelta + rule.DaylightDelta;
        var from = start;
        var to = start + rule.DaylightDelta;

        writer.WriteLine($"The UtcOffset then goes from  {nonDstOffset} to {dstOffset}");
        writer.WriteLine($"The Local Time then goes from {from.TimeOfDay} to {to.TimeOfDay}");
        writer.WriteLine();

        start = CalculateTransitionDate(year, rule.DaylightTransitionEnd);
        from = start;
        to = start - rule.DaylightDelta;

        writer.WriteLine($"The DST ends on {Explain(rule.DaylightTransitionEnd)}");

        writer.WriteLine($"The UtcOffset then goes from  {dstOffset} to {nonDstOffset}");
        writer.WriteLine($"The Local Time then goes from {from.TimeOfDay} to {to.TimeOfDay}");
        writer.WriteLine();
    }


    private static string Explain(TimeZoneInfo.TransitionTime tt)
        => tt.IsFixedDateRule
            ? $"the {tt.Day} of {CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(tt.Month)}"
            : $"the {ConvertToOrdinal(tt.Week)} {tt.DayOfWeek} of {CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(tt.Month)}";


    private static DateTime CalculateTransitionDate(int year, TimeZoneInfo.TransitionTime transitionTime)
    {
        DateTime transitionDate;

        if (transitionTime.IsFixedDateRule)
        {
            // Fixed date rule, just use the given month and day
            transitionDate = new DateTime(year, transitionTime.Month, transitionTime.Day);
        }
        else
        {
            // For relative date rules, we need to calculate based on the week and day of the week
            var firstDayOfMonth = new DateTime(year, transitionTime.Month, 1);
            var startOfWeek = firstDayOfMonth.AddDays(transitionTime.Week * 7 - 7);

            // Find the day in the correct week
            var daysToTargetDay = (7 - (int)startOfWeek.DayOfWeek + (int)transitionTime.DayOfWeek) % 7;
            transitionDate = startOfWeek.AddDays(daysToTargetDay);
        }

        // Apply the transition time to the calculated date
        transitionDate = transitionDate.Add(transitionTime.TimeOfDay.TimeOfDay);

        return transitionDate;
    }
    public static string ConvertToOrdinal(int number)
    {
        if (number < 0) throw new ArgumentOutOfRangeException(nameof(number), "Number must be non-negative.");

        // Handle special cases
        if (number % 100 >= 11 && number % 100 <= 13)
            return $"{number}th";

        // Determine the suffix for ordinal number
        return (number % 10) switch
        {
            1 => $"{number}st",
            2 => $"{number}nd",
            3 => $"{number}rd",
            _ => $"{number}th"
        };
    }
}