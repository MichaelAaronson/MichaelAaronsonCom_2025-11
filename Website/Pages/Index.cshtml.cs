using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Website.Pages
{
    public class IndexModel : PageModel
    {
        public string? SDatabaseConnection { get; set; }
        public void OnGet()
        {
            SDatabaseConnection = Environment.GetEnvironmentVariable("DatabaseConnection") ?? "Not Set";
        }
    }
}
