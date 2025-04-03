using System.CommandLine;
using System.CommandLine.Completions;

namespace MyTool.Commands.Root;

internal class DateCommand : Command
{
    private Argument<string> subjectArgument =
        new("subject", "The subject of the appointment.");
    private Option<DateTime> dateOption =
        new("--date", "The day of week to schedule. Should be within one week.");

    public DateCommand() : base("schedule", "Makes an appointment for sometime in the next week.")
    {
        AddArgument(subjectArgument);
        AddOption(dateOption);

        dateOption.AddCompletions((ctx) =>
        {
            var today = DateTime.Today;
            var dates = new List<CompletionItem>();
            foreach (var i in Enumerable.Range(1, 7))
            {
                var date = today.AddDays(i);
                dates.Add(new CompletionItem(
                    label: date.ToShortDateString(),
                    sortText: $"{i:2}"));
            }
            return dates;
        });

        this.SetHandler((subject, date) =>
            {
                Console.WriteLine($"Scheduled \"{subject}\" for {date}");
            },
            subjectArgument, dateOption);
    }
}