using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Pages.Persons
{
    public class IndexModel : PageModel
    {
        private readonly WebsiteContext _context;

        public IndexModel(WebsiteContext context)
        {
            _context = context;
        }

        public IList<Person> Person { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int? GroupId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ViewMode { get; set; } = "table";

        public SelectList GroupOptions { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var groups = await _context.Group.OrderBy(g => g.Name).ToListAsync();
            GroupOptions = new SelectList(groups, "Id", "Name");

            if (GroupId.HasValue)
            {
                Person = await _context.Person
                    .Where(p => p.PersonGroups.Any(pg => pg.GroupId == GroupId))
                    .OrderBy(p => p.FirstName)
                    .ThenBy(p => p.LastName)
                    .ToListAsync();
            }
            else
            {
                Person = await _context.Person
                    .OrderBy(p => p.FirstName)
                    .ThenBy(p => p.LastName)
                    .ToListAsync();
            }
        }
    }
}