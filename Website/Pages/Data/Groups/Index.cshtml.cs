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
    public class IndexModel : PageModel
    {
        private readonly Website.Data.WebsiteContext _context;

        public IndexModel(Website.Data.WebsiteContext context)
        {
            _context = context;
        }

        public IList<Group> Group { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Group = await _context.Group.ToListAsync();
        }
    }
}
