using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Website.Models;
using Website.Data;

namespace Website.Pages.PersonPages;

public class DetailsModel : PageModel
{
    private readonly WebsiteContext _context;
    public DetailsModel(WebsiteContext context)
    {
        _context = context;
    }

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
}
