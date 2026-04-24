using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Website.Data;
using Website.Models;

namespace Website.Pages_Data_Steps
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
        ViewData["DomainId"] = new SelectList(_context.Domain, "Id", "Title");
        ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Title");
            return Page();
        }

        [BindProperty]
        public Step Step { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Step.Add(Step);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
