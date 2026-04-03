using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Website.Models;

namespace Website.Pages.Persons
{
    public class EditModel : PageModel
    {
        private readonly Website.Data.WebsiteContext _context;

        public EditModel(Website.Data.WebsiteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Person Person { get; set; } = default!;

        [BindProperty]
        public List<int> SelectedGroupIds { get; set; } = new();

        public List<Group> AllGroups { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var person = await _context.Person
                .Include(p => p.PersonGroups)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (person == null)
                return NotFound();

            Person = person;
            AllGroups = await _context.Group.OrderBy(g => g.Name).ToListAsync();
            SelectedGroupIds = person.PersonGroups.Select(pg => pg.GroupId).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                AllGroups = await _context.Group.OrderBy(g => g.Name).ToListAsync();
                return Page();
            }

            var personToUpdate = await _context.Person
                .Include(p => p.PersonGroups)
                .FirstOrDefaultAsync(p => p.Id == Person.Id);

            if (personToUpdate == null)
                return NotFound();

            // Update scalar properties
            personToUpdate.FirstName = Person.FirstName;
            personToUpdate.LastName = Person.LastName;
            personToUpdate.Company = Person.Company;
            personToUpdate.Email = Person.Email;
            personToUpdate.Phone = Person.Phone;
            personToUpdate.Notes = Person.Notes;
            personToUpdate.ImageFilename = Person.ImageFilename;

            // Update group memberships
            personToUpdate.PersonGroups.Clear();
            foreach (var groupId in SelectedGroupIds)
            {
                personToUpdate.PersonGroups.Add(new PersonGroup
                {
                    PersonId = Person.Id,
                    GroupId = groupId
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
