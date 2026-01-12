using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Pages.Private.Groups
{
    public class DetailsModel : PageModel
    {
        private readonly Website.Data.WebsiteContext _context;

        public DetailsModel(Website.Data.WebsiteContext context)
        {
            _context = context;
        }

        public Group Group { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group = await _context.Group.FirstOrDefaultAsync(m => m.Id == id);

            if (group is not null)
            {
                Group = group;

                return Page();
            }

            return NotFound();
        }
    }
}
