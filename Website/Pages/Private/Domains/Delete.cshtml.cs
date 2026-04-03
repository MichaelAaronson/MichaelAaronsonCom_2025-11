using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Pages.Private.Domains
{
    public class DeleteModel : PageModel
    {
        private readonly Website.Data.WebsiteContext _context;

        public DeleteModel(Website.Data.WebsiteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Domain Domain { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domain = await _context.Domain.FirstOrDefaultAsync(m => m.Id == id);

            if (domain is not null)
            {
                Domain = domain;

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

            var domain = await _context.Domain.FindAsync(id);
            if (domain != null)
            {
                Domain = domain;
                _context.Domain.Remove(Domain);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
