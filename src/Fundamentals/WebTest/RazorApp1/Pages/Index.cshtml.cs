using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp1.Models;

namespace RazorApp1.Pages;
public class IndexModel : PageModel
{
    public void OnGet()
    {

    }

    public List<Person?> People { get; set; } =
    [
        new() { FirstName = "John",  LastName = "Travolta", Age = 55 , TimeSinceLastVisit = 12 },
        new() { FirstName = "Lady",  LastName = "Gaga", Age = 43, TimeSinceLastVisit = 5 },
        null,
        new() { FirstName = "Grote",  LastName = "Smurf", Age = 100, TimeSinceLastVisit = null },
    ];
}