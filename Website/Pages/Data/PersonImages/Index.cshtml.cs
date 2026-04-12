using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Pages.Data.PersonImages
{
    public class IndexModel : PageModel
    {
        private readonly Website.Data.WebsiteContext _context;

        public IndexModel(Website.Data.WebsiteContext context)
        {
            _context = context;
        }

        public IList<PersonImage> PersonImage { get;set; } = default!;

        public async Task OnGetAsync()
        {
            PersonImage = await _context.PersonImage
                .Include(p => p.Image)
                .Include(p => p.Person).ToListAsync();
        }
    }
}
