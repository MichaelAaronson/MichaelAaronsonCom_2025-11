using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Website.Models;
using Website.Data;

namespace Website.Pages.PersonPages;

public class DeleteModel : PageModel
{
    private readonly WebsiteContext _context;

    public DeleteModel(WebsiteContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Person Person { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var person = await _context.Person.FirstOrDefaultAsync(m => m.Id == id);
        if (person is null)
        {
            return NotFound();
        }
        else
        {
            Person = person;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var person = await _context.Person.FindAsync(id);
        if (person != null)
        {
            Person = person;
            _context.Person.Remove(Person);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
