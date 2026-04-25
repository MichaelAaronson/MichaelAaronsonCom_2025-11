using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Website.Models;
using Website.Data;

namespace Website.Pages.StepPages;

public class IndexModel : PageModel
{
    private readonly WebsiteContext _context;

    public IndexModel(WebsiteContext context)
    {
        _context = context;
    }

    public IList<Step> Step { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Step = await _context.Step.ToListAsync();
    }
}
