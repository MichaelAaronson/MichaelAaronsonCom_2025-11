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
    public class IndexModel : PageModel
    {
        private readonly Website.Data.WebsiteContext _context;

        public IndexModel(Website.Data.WebsiteContext context)
        {
            _context = context;
        }

        public IList<PlayNumber1> PlayNumber1 { get;set; } = default!;

        public async Task OnGetAsync()
        {
            PlayNumber1 = await _context.PlayNumber1.ToListAsync();
        }
    }
}
