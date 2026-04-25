using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Pages.Private
{
    [Authorize]
    public class PlanningModel : PageModel
    {
        private readonly WebsiteContext _context;

        public PlanningModel(WebsiteContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public bool ShowCompleted { get; set; } = false;

        [BindProperty(SupportsGet = true)]
        public string? SearchText { get; set; }

        // Domain -> Projects -> Steps hierarchy
        public List<DomainGroup> Domains { get; set; } = [];

        public int TotalSteps { get; set; }

        public class DomainGroup
        {
            public string DomainTitle { get; set; } = string.Empty;
            public List<ProjectGroup> Projects { get; set; } = [];
        }

        public class ProjectGroup
        {
            public int ProjectId { get; set; }
            public string ProjectTitle { get; set; } = string.Empty;
            public string? GoalTitle { get; set; }
            public int StepsTotal { get; set; }
            public int StepsComplete { get; set; }
            public int StepsActive { get; set; }
            public int StepsDeferred { get; set; }
            public List<Step> Steps { get; set; } = [];
        }

        public async Task OnGetAsync()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            // Always load all steps for accurate counts
            var allSteps = await _context.Step
                .Include(s => s.Domain)
                .Include(s => s.Project)
                .ThenInclude(p => p!.Goal)
                .OrderBy(s => s.Priority)
                .ThenBy(s => s.StartDate)
                .ToListAsync();
            // Apply search filter
            var searchTrimmed = SearchText?.Trim();
            if (!string.IsNullOrEmpty(searchTrimmed))
            {
                allSteps = allSteps
                    .Where(s => (s.Title?.Contains(searchTrimmed, StringComparison.OrdinalIgnoreCase) ?? false)
                                || (s.Comments?.Contains(searchTrimmed, StringComparison.OrdinalIgnoreCase) ?? false)
                                || (s.Project?.Title?.Contains(searchTrimmed, StringComparison.OrdinalIgnoreCase) ?? false))
                    .ToList();
            }

            // Group by Domain then Project
            Domains = allSteps
                .GroupBy(s => s.Domain?.Title ?? "(No Domain)")
                .OrderBy(g => g.Key)
                .Select(domainGroup => new DomainGroup
                {
                    DomainTitle = domainGroup.Key,
                    Projects = domainGroup
                        .GroupBy(s => new
                        {
                            ProjectId = s.Project?.Id ?? 0,
                            ProjectTitle = s.Project?.Title ?? "(No Project)",
                            GoalTitle = s.Project?.Goal?.Title
                        })
                        .OrderBy(g => g.Key.ProjectId == 0 ? 1 : 0)
                        .ThenBy(g => g.Key.ProjectTitle).Select(projGroup =>
                        {
                            var allProjectSteps = projGroup.ToList();
                            // Filter for display only
                            var displaySteps = ShowCompleted
                                ? allProjectSteps
                                : allProjectSteps.Where(s => !s.IsComplete).ToList();

                            return new ProjectGroup
                            {
                                ProjectId = projGroup.Key.ProjectId,
                                ProjectTitle = projGroup.Key.ProjectTitle,
                                GoalTitle = projGroup.Key.GoalTitle,
                                // Counts always based on ALL steps
                                StepsTotal = allProjectSteps.Count,
                                StepsComplete = allProjectSteps.Count(s => s.IsComplete),
                                StepsActive = allProjectSteps.Count(s => !s.IsComplete && s.StartDate <= today),
                                StepsDeferred = allProjectSteps.Count(s => !s.IsComplete && s.StartDate > today),
                                // Display only filtered steps
                                Steps = displaySteps
                            };
                        })
                        .Where(p => p.Steps.Count > 0) // Hide empty projects
                        .ToList()
                })
                .Where(d => d.Projects.Count > 0) // Hide empty domains
                .ToList();

            TotalSteps = Domains.SelectMany(d => d.Projects).SelectMany(p => p.Steps).Count();
        }
        public async Task<IActionResult> OnPostCompleteAsync(int id)
        {
            var step = await _context.Step.FindAsync(id);
            if (step != null)
            {
                step.IsComplete = true;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage(new { ShowCompleted, SearchText });
        }

        public async Task<IActionResult> OnPostChangePriorityAsync(
            [FromForm] int id,
            [FromForm] int newPriority)
        {
            var step = await _context.Step.FindAsync(id);
            if (step != null)
            {
                step.Priority = newPriority;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage(new { ShowCompleted, SearchText });
        }
    }
}