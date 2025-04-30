var tz = TimeZoneInfo.Local;
var now = DateTime.Now;
var utcNow = now.ToUniversalTime();
var offset = now - utcNow;

Console.WriteLine($"DisplayName:              {tz.DisplayName}");
Console.WriteLine($"Daylight Savings Support: {tz.SupportsDaylightSavingTime}");
Console.WriteLine($"Local Time:               {now}");
Console.WriteLine($"UTC Time:                 {utcNow}");
Console.WriteLine($"Offset:                   {offset}");
Console.WriteLine();