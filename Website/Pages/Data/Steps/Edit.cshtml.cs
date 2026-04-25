using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Website.Models;
using Website.Data;

namespace Website.Pages.StepPages;

public class EditModel : PageModel
{
    private readonly WebsiteContext _context;

    public EditModel(WebsiteContext context)
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
        Step = step;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Step).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StepExists(Step.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool StepExists(int id)
    {
        return _context.Step.Any(e => e.Id == id);
    }
}
