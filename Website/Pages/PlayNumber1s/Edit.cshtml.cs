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

namespace Website.Pages.PlayNumber1s
{
    public class EditModel : PageModel
    {
        private readonly Website.Data.WebsiteContext _context;

        public EditModel(Website.Data.WebsiteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PlayNumber1 PlayNumber1 { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playnumber1 =  await _context.PlayNumber1.FirstOrDefaultAsync(m => m.Id == id);
            if (playnumber1 == null)
            {
                return NotFound();
            }
            PlayNumber1 = playnumber1;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(PlayNumber1).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayNumber1Exists(PlayNumber1.Id))
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

        private bool PlayNumber1Exists(int id)
        {
            return _context.PlayNumber1.Any(e => e.Id == id);
        }
    }
}
