using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Pages.PlayNumbers
{
    public class DeleteModel : PageModel
    {
        private readonly Website.Data.WebsiteContext _context;

        public DeleteModel(Website.Data.WebsiteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PlayNumber PlayNumber { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playnumber = await _context.PlayNumber.FirstOrDefaultAsync(m => m.Id == id);

            if (playnumber is not null)
            {
                PlayNumber = playnumber;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playnumber = await _context.PlayNumber.FindAsync(id);
            if (playnumber != null)
            {
                PlayNumber = playnumber;
                _context.PlayNumber.Remove(PlayNumber);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
