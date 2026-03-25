using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Website.Data;
using Website.Models;

namespace Website.Pages.Private.JobSkills
{
    public class CreateModel : PageModel
    {
        private readonly Website.Data.WebsiteContext _context;

        public CreateModel(Website.Data.WebsiteContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public JobSkill JobSkill { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.JobSkill.Add(JobSkill);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
