using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Pages.Private.Steps
{
    public class DeleteModel : PageModel
    {
        private readonly Website.Data.WebsiteContext _context;

        public DeleteModel(Website.Data.WebsiteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Step Step { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var step = await _context.Step.FirstOrDefaultAsync(m => m.Id == id);

            if (step is not null)
            {
                Step = step;

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

            var step = await _context.Step.FindAsync(id);
            if (step != null)
            {
                Step = step;
                _context.Step.Remove(Step);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
