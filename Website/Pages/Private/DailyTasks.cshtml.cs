using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Pages.Private.Steps
{
    public class DailyTasksModel : PageModel
    {
        private readonly WebsiteContext _context;

        public DailyTasksModel(WebsiteContext context)
        {
            _context = context;
        }

        // The steps grouped by priority
        public List<IGrouping<int, Step>> GroupedSteps { get; set; } = [];

        // Current filter value (null = all priorities)
        [BindProperty(SupportsGet = true)]
        public int? MaxPriority { get; set; }

        public int TotalCount { get; set; }

        public async Task OnGetAsync()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            var query = _context.Step
                .Include(s => s.Project)
                    .ThenInclude(p => p!.Domain)
                .Include(s => s.Project)
                    .ThenInclude(p => p!.Goal)
                .Where(s => s.StartDate <= today && !s.IsComplete);

            // Apply priority filter if set
            if (MaxPriority.HasValue)
            {
                query = query.Where(s => s.Priority <= MaxPriority.Value);
            }

            var steps = await query
                .OrderBy(s => s.Priority)
                .ThenBy(s => s.Title)
                .ToListAsync();

            TotalCount = steps.Count;
            GroupedSteps = steps.GroupBy(s => s.Priority).OrderBy(g => g.Key).ToList();
        }

        public async Task<IActionResult> OnPostCompleteAsync(int id)
        {
            var step = await _context.Step.FindAsync(id);
            if (step != null)
            {
                step.IsComplete = true;
                await _context.SaveChangesAsync();
            }

            // Preserve the filter when redirecting back
            return RedirectToPage(new { MaxPriority });
        }
    }
}
