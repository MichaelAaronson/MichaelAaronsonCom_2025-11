using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Pages.Persons
{
    public class PersonsModel : PageModel
    {
        private readonly WebsiteContext _context;

        public PersonsModel(WebsiteContext context)
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

            IQueryable<Person> query = _context.Person;

            if (GroupId.HasValue)
            {
                query = query.Where(p => p.PersonGroups.Any(pg => pg.GroupId == GroupId));
            }

            if (ViewMode == "cards")
            {
                query = query
                    .Include(p => p.PersonImages)
                    .ThenInclude(pi => pi.Image);
            }

            Person = await query
                .OrderBy(p => p.FirstName)
                .ThenBy(p => p.LastName)
                .ToListAsync();
        }
    }
}