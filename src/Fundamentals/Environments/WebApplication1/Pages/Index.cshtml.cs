using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages;
public class IndexModel(ILogger<IndexModel> logger, IHostEnvironment environment, IConfiguration configuration) : PageModel
{
    public void OnGet()
    {
        Environment = environment;
        FavoriteNumber = configuration.GetValue<int>("FavoriteNumber");
    }

    public IHostEnvironment Environment;
    public int FavoriteNumber;
}
