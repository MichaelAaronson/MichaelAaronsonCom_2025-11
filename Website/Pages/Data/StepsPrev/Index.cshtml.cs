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
    public class IndexModel : PageModel
    {
        private readonly Website.Data.WebsiteContext _context;

        public IndexModel(Website.Data.WebsiteContext context)
        {
            _context = context;
        }

        public IList<Step> Step { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Step = await _context.Step
                .Include(s => s.Domain)
                .Include(s => s.Project).ToListAsync();
        }
    }
}
