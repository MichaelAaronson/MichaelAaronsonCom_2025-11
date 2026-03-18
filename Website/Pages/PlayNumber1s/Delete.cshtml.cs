using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Pages.PlayNumber1s
{
    public class DeleteModel : PageModel
    {
        private readonly Website.Data.WebsiteContext _context;

        public DeleteModel(Website.Data.WebsiteContext context)
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

            var playnumber1 = await _context.PlayNumber1.FirstOrDefaultAsync(m => m.Id == id);

            if (playnumber1 is not null)
            {
                PlayNumber1 = playnumber1;

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

            var playnumber1 = await _context.PlayNumber1.FindAsync(id);
            if (playnumber1 != null)
            {
                PlayNumber1 = playnumber1;
                _context.PlayNumber1.Remove(PlayNumber1);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
