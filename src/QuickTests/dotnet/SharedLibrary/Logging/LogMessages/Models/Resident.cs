namespace SharedLibrary.Logging.LogMessages.Models;

public record Resident
{
    public required string Name { get; set; }
    public required string CityOfResidence { get; set; }
}