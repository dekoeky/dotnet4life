using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace RequestLocalization.Controllers;

//[Route("{culture?}/[controller]")]
[Route("[controller]")]
[ApiController]
public sealed partial class GreetingController(
    IStringLocalizer<GreetingController> localizer,
    ILogger<GreetingController> logger) : ControllerBase
{
    [HttpGet]
    public string GetGreeting(string? name = null)
    {
        var localized = name is null
            ? localizer["HelloWorld"]
            : localizer["HelloName", name];

        if (localized.ResourceNotFound)
            logger.LogWarning("Localized String {Name} not found in {SearchedLocation} for culture {Culture}",
                localized.Name, localized.SearchedLocation, CultureInfo.CurrentCulture.Name);

        return localized.Value;
    }
}