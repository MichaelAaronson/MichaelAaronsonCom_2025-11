using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Website.Models;

namespace Website.Pages.Persons
{
    public class DetailsModel : PageModel
    {
        private readonly Website.Data.WebsiteContext _context;

        public DetailsModel(Website.Data.WebsiteContext context)
        {
            _context = context;
        }

        public Person Person { get; set; } = default!;
        public Image? MainImage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .Include(p => p.PersonImages)
                .ThenInclude(pi => pi.Image)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (person is not null)
            {
                Person = person;
                MainImage = person.PersonImages
                                .FirstOrDefault(pi => pi.IsMainImage)?.Image
                            ?? person.PersonImages.FirstOrDefault()?.Image;
                return Page();
            }

            return NotFound();
        }
    }
}