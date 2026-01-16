using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Pages.Private.Persons
{
    public class GroupMembersModel : PageModel
    {
        private readonly WebsiteContext _context;

        public GroupMembersModel(WebsiteContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int? GroupId { get; set; }

        public SelectList GroupOptions { get; set; } = default!;

        public Group? SelectedGroup { get; set; }

        public IList<Person> Members { get; set; } = new List<Person>();

        public async Task OnGetAsync()
        {
            // Populate the group dropdown
            var groups = await _context.Group.OrderBy(g => g.Name).ToListAsync();
            GroupOptions = new SelectList(groups, "Id", "Name");

            // If a group is selected, load its members
            if (GroupId.HasValue)
            {
                SelectedGroup = await _context.Group
                    .FirstOrDefaultAsync(g => g.Id == GroupId.Value);

                if (SelectedGroup != null)
                {
                    Members = await _context.Person
                        .Where(p => p.PersonGroups.Any(pg => pg.GroupId == GroupId.Value))
                        .OrderBy(p => p.FirstName)
                        .ThenBy(p => p.LastName)
                        .ToListAsync();
                }
            }
        }
    }
}