using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Pages_Data_Steps
{
    public class DetailsModel : PageModel
    {
        private readonly Website.Data.WebsiteContext _context;

        public DetailsModel(Website.Data.WebsiteContext context)
        {
            _context = context;
        }

        public Step Step { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var step = await _context.Step
                .Include(s => s.Project)
                .Include(s => s.Domain)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (step is not null)
            {
                Step = step;

                return Page();
            }

            return NotFound();
        }
    }
}
