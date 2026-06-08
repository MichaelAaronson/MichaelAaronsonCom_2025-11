using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Website.Models;
using Website.Data;

namespace Website.Pages.StepPages;

public class DetailsModel : PageModel
{
    private readonly WebsiteContext _context;
    public DetailsModel(WebsiteContext context)
    {
        _context = context;
    }

    public Step Step { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var step = await _context.Step.FirstOrDefaultAsync(m => m.Id == id);
        if (step is null)
        {
            return NotFound();
        }
        else
        {
            Step = step;
        }

        return Page();
    }
}
