using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Pages.Persons
{
    public class CreateModel : PageModel
    {
        private readonly Website.Data.WebsiteContext _context;

        public CreateModel(Website.Data.WebsiteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Person Person { get; set; } = default!;

        [BindProperty]
        public List<int> SelectedGroupIds { get; set; } = new();

        [BindProperty]
        public int? SelectedImageId { get; set; }

        public List<Group> AllGroups { get; set; } = new();
        public List<Image> AllImages { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            AllGroups = await _context.Group.OrderBy(g => g.Name).ToListAsync();
            AllImages = await _context.Image.OrderBy(i => i.Description).ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                AllGroups = await _context.Group.OrderBy(g => g.Name).ToListAsync();
                AllImages = await _context.Image.OrderBy(i => i.Description).ToListAsync();
                return Page();
            }

            _context.Person.Add(Person);
            await _context.SaveChangesAsync();

            // Add group memberships
            foreach (var groupId in SelectedGroupIds)
            {
                _context.PersonGroup.Add(new PersonGroup
                {
                    PersonId = Person.Id,
                    GroupId = groupId
                });
            }

            // Add main image
            if (SelectedImageId.HasValue)
            {
                _context.PersonImage.Add(new PersonImage
                {
                    PersonId = Person.Id,
                    ImageId = SelectedImageId.Value,
                    IsMainImage = true
                });
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}