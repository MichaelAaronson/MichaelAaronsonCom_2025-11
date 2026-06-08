using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Website.Models;
using Website.Data;

namespace Website.Pages.PersonPages;

public class IndexModel : PageModel
{
    private readonly WebsiteContext _context;

    public IndexModel(WebsiteContext context)
    {
        _context = context;
    }

    public IList<Person> Person { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Person = await _context.Person.ToListAsync();
    }
}
