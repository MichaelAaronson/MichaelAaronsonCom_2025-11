using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Pages.Private.ContentEditor
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly WebsiteContext _context;

        public IndexModel(WebsiteContext context)
        {
            _context = context;
        }

        public List<ContentBlock> ContentBlocks { get; set; } = new();
        public string? Message { get; set; }

        public async Task OnGetAsync()
        {
            ContentBlocks = await _context.ContentBlock
                .OrderBy(c => c.Title)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var block = await _context.ContentBlock.FindAsync(id);
            if (block != null)
            {
                _context.ContentBlock.Remove(block);
                await _context.SaveChangesAsync();
                Message = $"Deleted: {block.Key}";
            }

            ContentBlocks = await _context.ContentBlock
                .OrderBy(c => c.Title)
                .ToListAsync();

            return Page();
        }
    }
}