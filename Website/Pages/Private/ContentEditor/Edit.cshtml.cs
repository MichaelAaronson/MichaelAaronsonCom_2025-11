using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Pages.Private.ContentEditor
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly WebsiteContext _context;

        public EditModel(WebsiteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ContentBlock ContentBlock { get; set; } = new();

        public bool IsNew => ContentBlock.Id == 0;
        public string? Message { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id.HasValue)
            {
                var block = await _context.ContentBlock.FindAsync(id.Value);
                if (block == null)
                {
                    return NotFound();
                }
                ContentBlock = block;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Normalise the key: lowercase, trim
            ContentBlock.Key = ContentBlock.Key.Trim().ToLowerInvariant();

            if (ContentBlock.Id == 0)
            {
                // Creating new - check for duplicate key
                var existing = await _context.ContentBlock
                    .AnyAsync(c => c.Key == ContentBlock.Key);

                if (existing)
                {
                    ModelState.AddModelError("ContentBlock.Key",
                        $"A content block with key '{ContentBlock.Key}' already exists.");
                    return Page();
                }

                ContentBlock.LastModified = DateTime.UtcNow;
                _context.ContentBlock.Add(ContentBlock);
            }
            else
            {
                // Updating existing - check key isn't taken by another block
                var duplicate = await _context.ContentBlock
                    .AnyAsync(c => c.Key == ContentBlock.Key && c.Id != ContentBlock.Id);

                if (duplicate)
                {
                    ModelState.AddModelError("ContentBlock.Key",
                        $"Another content block already uses the key '{ContentBlock.Key}'.");
                    return Page();
                }

                var blockToUpdate = await _context.ContentBlock.FindAsync(ContentBlock.Id);
                if (blockToUpdate == null)
                {
                    return NotFound();
                }

                blockToUpdate.Key = ContentBlock.Key;
                blockToUpdate.Title = ContentBlock.Title;
                blockToUpdate.MarkdownContent = ContentBlock.MarkdownContent;
                blockToUpdate.LastModified = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
