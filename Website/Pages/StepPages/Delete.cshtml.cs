using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Website.Models;
using Website.Data;

namespace Website.Pages.StepPages;

public class DeleteModel : PageModel
{
    private readonly WebsiteContext _context;

    public DeleteModel(WebsiteContext context)
    {
        _context = context;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var step = await _context.Step.FindAsync(id);
        if (step != null)
        {
            Step = step;
            _context.Step.Remove(Step);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
