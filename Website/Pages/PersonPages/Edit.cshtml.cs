using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Website.Models;
using Website.Data;

namespace Website.Pages.PersonPages;

public class EditModel : PageModel
{
    private readonly WebsiteContext _context;

    public EditModel(WebsiteContext context)
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
        Person = person;
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

        _context.Attach(Person).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PersonExists(Person.Id))
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

    private bool PersonExists(int id)
    {
        return _context.Person.Any(e => e.Id == id);
    }
}
